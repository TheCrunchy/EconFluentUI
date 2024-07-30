using Econ.Interfaces;
using Econ.Models.Stores;
using Econ.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FluentUI.Components.Stores
{
    public partial class LiveStoreDataComponent
    {
        [Parameter] public Guid ServerId { get; set; }


        [Inject] private LiveStoreDataService _storeLogsService { get; set; }
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private IQueryable<KeenNPCStoreEntry> GridData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _storeLogsService.DataRefreshed += async (Guid id) => { await LoadData(id); };
        }

        public async Task LoadData(Guid serverId)
        {
            if (serverId == ServerId)
            {
                var data = _storeLogsService.GetKeenStoreEntries(ServerId);
                GridData = data.AsQueryable();
                await InvokeAsync(StateHasChanged);

            }

        }
    }
}
