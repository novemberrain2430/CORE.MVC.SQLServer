using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Sample.Books;

namespace Sample.Books
{
    public class BooksDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IBookRepository _bookRepository;

        public BooksDataSeedContributor(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19"),
                name: "56d51dd44ef444f2861fe51f1f81976962bcea16d28c4b48bef4b9d",
                code: "013f55744df34339a1aa1f0becd0f83d5a0aca8518a243f"
            ));

            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("07def331-64a9-4347-999c-b3c2ab5dc1f7"),
                name: "e398d7e8f4d34f49999113b7e3bc16cb26283190d91749b7b68c30d6e4d77abeed5085709f234c968a95186",
                code: "fbaded55eefb4c288aa39678065b4d2dd21587"
            ));
        }
    }
}