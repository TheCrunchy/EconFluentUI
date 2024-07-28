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
        [Parameter] public Action Changed { get; set; } 

        [Inject] private IStoreLogService _storeLogsService { get; set; }

        private IQueryable<StoreTransaction> GridData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Changed = async () => await LoadData();
        }

        public async Task LoadData()
        {
            var data = await _storeLogsService.GetTransactionsAsync(ServerId);
            GridData = data.AsQueryable();
        }
    }
}
