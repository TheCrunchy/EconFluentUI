using Econ.Interfaces;
using Econ.Models.Stores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FluentUI.Components.Statistics
{
    public partial class StoresTransactionsComponent
    {
        [Parameter] public Guid ServerId { get; set; }

        [Inject] private IStoreTransactionStatisticsService _transactionService { get; set; }

        private IQueryable<StoreTransaction> GridData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var data = await _transactionService.ProcessFilesInParallel(@"C:\Users\Cameron\Documents\4 Torch Server\Instance\CrunchEconV3\EconEntries");
            GridData = data.Values.AsQueryable();
        }
    }
}
