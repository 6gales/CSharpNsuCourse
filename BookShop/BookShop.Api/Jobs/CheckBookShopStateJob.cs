using System.Threading.Tasks;
using BookShop.Logic;
using Quartz;

namespace BookShop.Api.Jobs
{
    [DisallowConcurrentExecution]
    public sealed class CheckBookShopStateJob : IJob
    {
        private readonly BookShopService _bookShopService;

        public CheckBookShopStateJob(BookShopService bookShopService)
        {
            _bookShopService = bookShopService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _bookShopService.UpdateSystem(context.FireTimeUtc.DateTime);
        }
    }
}
