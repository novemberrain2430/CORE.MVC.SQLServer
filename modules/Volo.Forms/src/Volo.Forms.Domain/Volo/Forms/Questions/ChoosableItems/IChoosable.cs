using System;
using System.Collections.Generic;
using Volo.Forms.Choices;

namespace Volo.Forms.Questions.ChoosableItems
{
    public interface IChoosable
    {
        public void AddChoice(Guid id, int index, string value, bool isCorrect = false);
        public void AddChoices(List<(Guid id, string value, bool isCorrect)> choices);
        public void MoveChoice(Guid id, int newIndex);
        public ICollection<Choice> GetChoices();
        public void ClearChoices();
        // public void SetChoiceValues(List<string> values);
    }
}