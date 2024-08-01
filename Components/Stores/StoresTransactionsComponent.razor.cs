using Econ.Interfaces;
using Econ.Models.Stores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FluentUI.Components.Stores
{
    public partial class StoresTransactionsComponent
    {
        [Parameter] public Guid ServerId { get; set; } 
        [Parameter] public Action Changed { get; set; } 

        [Inject] private IStoreLogService _storeLogsService { get; set; }
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private IQueryable<StoreTransaction> GridData { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task LoadData()
        {
       //     var data = await _storeLogsService.GetTransactionsAsync(ServerId);
        //    GridData = data.AsQueryable();
        //    await InvokeAsync(StateHasChanged);
        }
    }
}
