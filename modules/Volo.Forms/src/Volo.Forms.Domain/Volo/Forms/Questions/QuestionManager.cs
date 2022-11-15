﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Forms.Choices;
using Volo.Forms.Forms;
using Volo.Forms.Questions.ChoosableItems;

namespace Volo.Forms.Questions
{
    public class QuestionManager : DomainService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IFormRepository _formRepository;
        private readonly IGuidGenerator _guidGenerator;

        public QuestionManager(
            IQuestionRepository questionRepository,
            IFormRepository formRepository,
            IGuidGenerator guidGenerator)
        {
            _questionRepository = questionRepository;
            _formRepository = formRepository;
            _guidGenerator = guidGenerator;
        }

        public virtual async Task<QuestionBase> CreateQuestionAsync(
            Form form,
            QuestionTypes questionType,
            int index,
            bool isRequired,
            string title,
            string description,
            bool hasOtherOption,
            List<(string value, bool isCorrect)> choices
        )
        {
            await UpdateFormLastModificationDateAsync(form.Id);

            return await InsertAsync(form.Id, questionType, index, isRequired, title, description, hasOtherOption, choices, null);
        }

        public virtual async Task<QuestionBase> UpdateAsync(
            Guid id,
            string title,
            int index,
            bool isRequired,
            string description,
            QuestionTypes questionType,
            bool hasOtherOption,
            List<(Guid Id, string value, bool isCorrect)> choiceList)
        {
            var question = await _questionRepository.GetAsync(id);
            var questionId = question.Id;
            var formId = question.FormId;
            var creationDate = question.CreationTime;

            // This will be removed when EfCore bug is resolved: https://github.com/dotnet/efcore/issues/22016
            // Since item with choice collection can't return single object; we can't remove choices over item.
            await ClearItemChoicesAsync(question);

            question = await _questionRepository.GetAsync(id);
            await _questionRepository.HardDeleteAsync(question, autoSave: true);

            var createdQuestion = CreateItemBasedOnType(questionType, questionId);
            createdQuestion
                .SetFormId(formId)
                .SetTitle(title)
                .SetIndex(index)
                .SetDescription(description);
            createdQuestion.SetRequired(isRequired);
            createdQuestion.SetOtherOption(hasOtherOption);

            if (createdQuestion is IChoosable choosableItem)
            {
                if (!(question is IChoosable))
                {
                    for (var i = 0; i < choiceList.Count; i++)
                    {
                        choosableItem
                            .AddChoice(_guidGenerator.Create(), i + 1, choiceList[i].value, choiceList[i].isCorrect);
                    }
                }
                else
                {
                    UpdateIndexesOfChoiceList(choiceList);
                    choosableItem.AddChoices(choiceList);
                }
            }

            createdQuestion.CreationTime = creationDate;
            createdQuestion.LastModificationTime = DateTime.Now;
            await UpdateFormLastModificationDateAsync(createdQuestion.FormId);

            return await _questionRepository.InsertAsync(createdQuestion, true);
        }

        public virtual async Task DeleteAsync(QuestionBase question)
        {
            await _questionRepository.DeleteAsync(question, autoSave: true);
            await UpdateFormLastModificationDateAsync(question.FormId);
        }

        protected virtual async Task UpdateFormLastModificationDateAsync(Guid createdQuestionFormId)
        {
            var form = await _formRepository.FindAsync(createdQuestionFormId);

            form.LastModificationTime = DateTime.Now;

            await _formRepository.UpdateAsync(form);
        }

        [UnitOfWork]
        public virtual async Task ClearItemChoicesAsync(QuestionBase question)
        {
            await _questionRepository.ClearQuestionChoicesAsync(question.Id);
        }

        protected virtual async Task<QuestionBase> InsertAsync(
            Guid formId,
            QuestionTypes questionType,
            int index,
            bool isRequired,
            string title,
            string description,
            bool hasOtherOption,
            List<(string value, bool isCorrect)> choices,
            Guid? id)
        {
            var createdQuestion = CreateItemBasedOnType(questionType, id);

            createdQuestion
                .SetFormId(formId)
                .SetTitle(title)
                .SetIndex(index)
                .SetDescription(description);

            createdQuestion.SetRequired(isRequired);
            createdQuestion.SetOtherOption(hasOtherOption);

            if (createdQuestion is IChoosable choosableItem)
            {
                for (var i = 0; i < choices.Count; i++)
                {
                    choosableItem.AddChoice(_guidGenerator.Create(), i + 1, choices[i].value, choices[i].isCorrect);
                }
            }

            return await _questionRepository.InsertAsync(createdQuestion, true);
        }

        protected virtual void UpdateIndexesOfChoiceList(List<(Guid Id, string value, bool isCorrect)> choiceList)
        {
            var otherChoice = choiceList.FirstOrDefault(q => q.value == ChoiceConsts.OtherChoice);

            if (otherChoice.value == null)
            {
                return;
            }

            choiceList.Remove(otherChoice);
            choiceList.Add(otherChoice);
        }

        protected virtual QuestionBase CreateItemBasedOnType(QuestionTypes questionType, Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                id = _guidGenerator.Create();
            }

            return questionType switch
            {
                QuestionTypes.ShortText => new ShortText(id.Value, CurrentTenant.Id),
                QuestionTypes.Checkbox => new Checkbox(id.Value, CurrentTenant.Id),
                QuestionTypes.ChoiceMultiple => new ChoiceMultiple(id.Value, CurrentTenant.Id),
                QuestionTypes.DropdownList => new DropdownList(id.Value, CurrentTenant.Id),
                _ => new ShortText(id.Value, CurrentTenant.Id)
            };
        }
    }
}
