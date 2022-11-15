using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using CORE.MVC.SQLServer.Samples;

namespace CORE.MVC.SQLServer.Samples
{
    public class SamplesDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ISampleRepository _sampleRepository;

        public SamplesDataSeedContributor(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _sampleRepository.InsertAsync(new Sample
            (
                id: Guid.Parse("335f3648-f278-4c2d-a1ca-b465d0e942b9"),
                name: "0a45fe1f0b014f9db51f409807080833aa695c0d4b9946558a3",
                date1: new DateTime(2007, 7, 24),
                year: 1915053339,
                code: "c6c46982868e44b48fa22f650b154566acc6bce00dde4c6eb75df39e13c4e7fedbad945ef52c46b7aeaf337945e00effa33c9e6d5a5b4fdc8e16ac2dac5498ad0a8ade7bda364b77900436574ec983cae31b0fa9a1cb48f1977e720030f90b6801ec6066",
                email: "aeb4db18749e47f3b6c906feca9318cf641@c58f69066a2c4b94a32e5cccfccdaea40d8.com",
                isConfirm: true,
                userId: Guid.Parse("19612a4a-a8de-47d0-8f26-fe24758564a7")
            ));

            await _sampleRepository.InsertAsync(new Sample
            (
                id: Guid.Parse("3895c5ea-1d49-422f-93ee-2f84b6941e8f"),
                name: "684b7e4c5cca4bb9bc051",
                date1: new DateTime(2008, 4, 15),
                year: 1020189076,
                code: "7fe4af11d32f424581532a90fb29fb312ba11ff4c67240be81cd5832f001829fb0d45f10b6f54008bf53429018d10c7ddd204d3c91fb41fcaa67e6815e4b8ede140cccf884d841febaa546aec2391fdfe483cbc3ec934c8b98fd96224fdc4baeae0414fc",
                email: "3bab86151d4842dc9d4cca7959d00237bc2577@4e752ff70006409398ba3d3a43c79dc02cec41.com",
                isConfirm: true,
                userId: Guid.Parse("35e40eee-3dca-4037-863a-48d13039ee83")
            ));
        }
    }
}