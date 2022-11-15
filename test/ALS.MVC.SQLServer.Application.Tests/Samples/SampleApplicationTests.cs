using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace CORE.MVC.SQLServer.Samples
{
    public class SamplesAppServiceTests : SQLServerApplicationTestBase
    {
        private readonly ISamplesAppService _samplesAppService;
        private readonly IRepository<Sample, Guid> _sampleRepository;

        public SamplesAppServiceTests()
        {
            _samplesAppService = GetRequiredService<ISamplesAppService>();
            _sampleRepository = GetRequiredService<IRepository<Sample, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _samplesAppService.GetListAsync(new GetSamplesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("335f3648-f278-4c2d-a1ca-b465d0e942b9")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("3895c5ea-1d49-422f-93ee-2f84b6941e8f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _samplesAppService.GetAsync(Guid.Parse("335f3648-f278-4c2d-a1ca-b465d0e942b9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("335f3648-f278-4c2d-a1ca-b465d0e942b9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SampleCreateDto
            {
                Name = "871cd08a8e954292b6",
                Date1 = new DateTime(2013, 2, 6),
                Year = 176555109,
                Code = "dbc8be66c4db4d5d8f235ee343b2d6d29f4e44bad0db48c1bc09e5d87cb198890b842e79d64d4c3d9aa27fa60161e6c4ef1cea6fd5534f9aae3cb22ca9e544ad42c3df60023d4f25bc3a775a23e41effc8475dbc92da4b6988a4a639c519329420881ddb",
                Email = "39ec5ec030c649d091ba@b8cdad4b1b4647808a8a.com",
                IsConfirm = true,
                UserId = Guid.Parse("2ee7936d-861a-4333-a92b-4cae5632af15")
            };

            // Act
            var serviceResult = await _samplesAppService.CreateAsync(input);

            // Assert
            var result = await _sampleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("871cd08a8e954292b6");
            result.Date1.ShouldBe(new DateTime(2013, 2, 6));
            result.Year.ShouldBe(176555109);
            result.Code.ShouldBe("dbc8be66c4db4d5d8f235ee343b2d6d29f4e44bad0db48c1bc09e5d87cb198890b842e79d64d4c3d9aa27fa60161e6c4ef1cea6fd5534f9aae3cb22ca9e544ad42c3df60023d4f25bc3a775a23e41effc8475dbc92da4b6988a4a639c519329420881ddb");
            result.Email.ShouldBe("39ec5ec030c649d091ba@b8cdad4b1b4647808a8a.com");
            result.IsConfirm.ShouldBe(true);
            result.UserId.ShouldBe(Guid.Parse("2ee7936d-861a-4333-a92b-4cae5632af15"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SampleUpdateDto()
            {
                Name = "e5ec5f75f9c34a758b7dfa574eca85a78ca13cea4a95492896ba457c62b957fef68a4c58e",
                Date1 = new DateTime(2009, 8, 9),
                Year = 1078294980,
                Code = "0078191c4d9047e1b423213ec5d44defb069ec3e80c14668ad9ba1e1b9b97f94cdcd7226fd0f4d3cb9fec205308568ac93e29279ddbc46989281fb8e2a43bd98958ef32bfe85450ea85321f9390b8964b1d286e534bc4f34ae7858c3604edd5596b2f4df",
                Email = "f6b6accc59494793@104819b84882432e.com",
                IsConfirm = true,
                UserId = Guid.Parse("1f589041-f801-4ee0-90c2-38474d23d7db")
            };

            // Act
            var serviceResult = await _samplesAppService.UpdateAsync(Guid.Parse("335f3648-f278-4c2d-a1ca-b465d0e942b9"), input);

            // Assert
            var result = await _sampleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("e5ec5f75f9c34a758b7dfa574eca85a78ca13cea4a95492896ba457c62b957fef68a4c58e");
            result.Date1.ShouldBe(new DateTime(2009, 8, 9));
            result.Year.ShouldBe(1078294980);
            result.Code.ShouldBe("0078191c4d9047e1b423213ec5d44defb069ec3e80c14668ad9ba1e1b9b97f94cdcd7226fd0f4d3cb9fec205308568ac93e29279ddbc46989281fb8e2a43bd98958ef32bfe85450ea85321f9390b8964b1d286e534bc4f34ae7858c3604edd5596b2f4df");
            result.Email.ShouldBe("f6b6accc59494793@104819b84882432e.com");
            result.IsConfirm.ShouldBe(true);
            result.UserId.ShouldBe(Guid.Parse("1f589041-f801-4ee0-90c2-38474d23d7db"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _samplesAppService.DeleteAsync(Guid.Parse("335f3648-f278-4c2d-a1ca-b465d0e942b9"));

            // Assert
            var result = await _sampleRepository.FindAsync(c => c.Id == Guid.Parse("335f3648-f278-4c2d-a1ca-b465d0e942b9"));

            result.ShouldBeNull();
        }
    }
}