using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using CORE.MVC.SQLServer.Xamples;
using CORE.MVC.SQLServer.EntityFrameworkCore;
using Xunit;

namespace CORE.MVC.SQLServer.Xamples
{
    public class XampleRepositoryTests : SQLServerEntityFrameworkCoreTestBase
    {
        private readonly IXampleRepository _xampleRepository;

        public XampleRepositoryTests()
        {
            _xampleRepository = GetRequiredService<IXampleRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _xampleRepository.GetListAsync(
                    name: "f64890db9ae6447cb63b39f6594b02b3",
                    code: "8e5a17d64f414548b3df6c18caaa7966255da2b78a344b998acb63976500034fe0ff0d61af9348dbb3236636d752c94389d88784bcdd43d582509c3dba11602ea51a80f9a70949c7af9427536642bd407fb8edfbb0b34f4386d97e0f27b6f7b8248e9f35",
                    email: "390c6417da0a412885163474a146df47544@5c130530612a44128cbfcb1452f79d1ef5c.com",
                    isConfirm: true,
                    userId: Guid.Parse("80a26739-89c2-40f8-a405-ea0f3bf6a11a")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("bd272f32-fe4f-4a27-a648-d6ba6a8f6a02"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _xampleRepository.GetCountAsync(
                    name: "cb5dd743ff484e459f8ed3a0de26e2d7c4136df",
                    code: "6b46b2fbbc75424fb2005fcfbb35579d14e25cd7e58d4039b35928f18c60763d1f7f7c9480a04f1583980b35f8efe6179a9f711a662242b4a0affb1119eae3062bc2017230b24595b898085895ddc2a6bff43d7dd9b7471cbc3e606ef7c4a12eea12c6ce",
                    email: "815f67b6f3e74ace9ae94590bb448c5dbbe57d035f024@6f45d0b618d2438c99a8944d601064ca2210dc3b880c4.com",
                    isConfirm: true,
                    userId: Guid.Parse("4c730f97-5082-4c6e-b71d-89574734c966")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}