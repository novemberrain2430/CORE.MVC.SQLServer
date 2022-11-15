using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Sample.Books
{
    public class BooksAppServiceTests : SampleApplicationTestBase
    {
        private readonly IBooksAppService _booksAppService;
        private readonly IRepository<Book, Guid> _bookRepository;

        public BooksAppServiceTests()
        {
            _booksAppService = GetRequiredService<IBooksAppService>();
            _bookRepository = GetRequiredService<IRepository<Book, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _booksAppService.GetListAsync(new GetBooksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("07def331-64a9-4347-999c-b3c2ab5dc1f7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _booksAppService.GetAsync(Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BookCreateDto
            {
                Name = "d2239ab881af4cde92d8466e0d3b2c5bbabf54562da540de9235b6dd01c03c671c7d7",
                Code = "915d8a31243c4f309a9adbb0f36efb6b954d962d228343cba7099b20e1c2bb8031bf7911439448ca8d5466"
            };

            // Act
            var serviceResult = await _booksAppService.CreateAsync(input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("d2239ab881af4cde92d8466e0d3b2c5bbabf54562da540de9235b6dd01c03c671c7d7");
            result.Code.ShouldBe("915d8a31243c4f309a9adbb0f36efb6b954d962d228343cba7099b20e1c2bb8031bf7911439448ca8d5466");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BookUpdateDto()
            {
                Name = "7c0aaebd0bb446caad78846bcbc888c38270a8fd17634dbfabc9e4a59351fe",
                Code = "f9d901f8b8d74cd88f5109b28a"
            };

            // Act
            var serviceResult = await _booksAppService.UpdateAsync(Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19"), input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("7c0aaebd0bb446caad78846bcbc888c38270a8fd17634dbfabc9e4a59351fe");
            result.Code.ShouldBe("f9d901f8b8d74cd88f5109b28a");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _booksAppService.DeleteAsync(Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19"));

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == Guid.Parse("68d42f3f-fc85-4ab7-aab5-53b824c2eb19"));

            result.ShouldBeNull();
        }
    }
}