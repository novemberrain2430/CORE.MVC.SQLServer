using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace CORE.MVC.SQLServer.Xamples
{
    public class XamplesAppServiceTests : SQLServerApplicationTestBase
    {
        private readonly IXamplesAppService _xamplesAppService;
        private readonly IRepository<Xample, Guid> _xampleRepository;

        public XamplesAppServiceTests()
        {
            _xamplesAppService = GetRequiredService<IXamplesAppService>();
            _xampleRepository = GetRequiredService<IRepository<Xample, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _xamplesAppService.GetListAsync(new GetXamplesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("bd272f32-fe4f-4a27-a648-d6ba6a8f6a02")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("95452515-ce5f-4e55-acd0-add0c7501ef4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _xamplesAppService.GetAsync(Guid.Parse("bd272f32-fe4f-4a27-a648-d6ba6a8f6a02"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bd272f32-fe4f-4a27-a648-d6ba6a8f6a02"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new XampleCreateDto
            {
                Name = "542396183db34b52",
                Date1 = new DateTime(2007, 6, 16),
                Year = 694819404,
                Code = "b3768a4ed9454710910511d9c14b971f469bb361eccf410f81b1bc31228e5c2f53c6de34ff9a471cadd76c4d92cc57d771c44ac022eb4062af9769ff8ebf7c638f139822c4f248239aa7742ab32e10e0a8dc4e01a9c842ada8fe08b938f08a7d296c60cc",
                Email = "ba0cfbf94e624d2ca6eb6f1@5bf1c47e15b64959a648d4d.com",
                IsConfirm = true,
                UserId = Guid.Parse("897cb644-b445-4a57-9143-6fb28612d444")
            };

            // Act
            var serviceResult = await _xamplesAppService.CreateAsync(input);

            // Assert
            var result = await _xampleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("542396183db34b52");
            result.Date1.ShouldBe(new DateTime(2007, 6, 16));
            result.Year.ShouldBe(694819404);
            result.Code.ShouldBe("b3768a4ed9454710910511d9c14b971f469bb361eccf410f81b1bc31228e5c2f53c6de34ff9a471cadd76c4d92cc57d771c44ac022eb4062af9769ff8ebf7c638f139822c4f248239aa7742ab32e10e0a8dc4e01a9c842ada8fe08b938f08a7d296c60cc");
            result.Email.ShouldBe("ba0cfbf94e624d2ca6eb6f1@5bf1c47e15b64959a648d4d.com");
            result.IsConfirm.ShouldBe(true);
            result.UserId.ShouldBe(Guid.Parse("897cb644-b445-4a57-9143-6fb28612d444"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new XampleUpdateDto()
            {
                Name = "0acbb437b3e7454ab959d4f3df4c6a5a3a11a506ab884f30b15b90f6f5b63c9cc895d74627b34d5caa8d6f",
                Date1 = new DateTime(2010, 9, 17),
                Year = 288166937,
                Code = "fad54628a10f4da4832d131b3caf239d5d442df1fcbb482b9a57ffcd5221bfbfb2f3ece0cd514e5ea0529a4dd746f8352447dd1f36714a34b2933e43e0393679f14a13cc543947c6aa7620fad31705d3ddb3e2391bc242858abd24a59118b6fac7f5d08c",
                Email = "1880eed99e@0f8fc5e2f0.com",
                IsConfirm = true,
                UserId = Guid.Parse("3927e4b9-f5c3-48c0-806c-7cc2602690ac")
            };

            // Act
            var serviceResult = await _xamplesAppService.UpdateAsync(Guid.Parse("bd272f32-fe4f-4a27-a648-d6ba6a8f6a02"), input);

            // Assert
            var result = await _xampleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("0acbb437b3e7454ab959d4f3df4c6a5a3a11a506ab884f30b15b90f6f5b63c9cc895d74627b34d5caa8d6f");
            result.Date1.ShouldBe(new DateTime(2010, 9, 17));
            result.Year.ShouldBe(288166937);
            result.Code.ShouldBe("fad54628a10f4da4832d131b3caf239d5d442df1fcbb482b9a57ffcd5221bfbfb2f3ece0cd514e5ea0529a4dd746f8352447dd1f36714a34b2933e43e0393679f14a13cc543947c6aa7620fad31705d3ddb3e2391bc242858abd24a59118b6fac7f5d08c");
            result.Email.ShouldBe("1880eed99e@0f8fc5e2f0.com");
            result.IsConfirm.ShouldBe(true);
            result.UserId.ShouldBe(Guid.Parse("3927e4b9-f5c3-48c0-806c-7cc2602690ac"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _xamplesAppService.DeleteAsync(Guid.Parse("bd272f32-fe4f-4a27-a648-d6ba6a8f6a02"));

            // Assert
            var result = await _xampleRepository.FindAsync(c => c.Id == Guid.Parse("bd272f32-fe4f-4a27-a648-d6ba6a8f6a02"));

            result.ShouldBeNull();
        }
    }
}