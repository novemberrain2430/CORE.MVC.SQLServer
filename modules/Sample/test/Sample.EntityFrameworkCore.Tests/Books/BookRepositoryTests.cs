using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sample.Books;
using Sample.EntityFrameworkCore;
using Xunit;

namespace Sample.Books
{
    public class BookRepositoryTests : SampleEntityFrameworkCoreTestBase
    {
        private readonly IBookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _bookRepository = GetRequiredService<IBookRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetListAsync(
                    name: "56d51dd44ef444f2861fe51f1f81976962bcea16d28c4b48bef4b9d",
                    code: "013f55744df34339a1aa1f0becd0f83d5a0aca8518a243f"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetCountAsync(
                    name: "e398d7e8f4d34f49999113b7e3bc16cb26283190d91749b7b68c30d6e4d77abeed5085709f234c968a95186",
                    code: "fbaded55eefb4c288aa39678065b4d2dd21587"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}