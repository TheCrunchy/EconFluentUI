using Econ.Interfaces;
using Econ.Models.Stores;
using Econ.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FluentUI.Components.Stores
{
    public partial class LiveStoreDataComponent
    {
        [Parameter] public Guid ServerId { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }


        [Inject] private LiveStoreDataService _storeLogsService { get; set; }
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private IQueryable<KeenNPCStoreEntry> UnfilteredData { get; set; }
        private IQueryable<KeenNPCStoreEntry> GridData
        {
            get
            {
                if (UnfilteredData != null)
                {
                    var result = UnfilteredData;
                    if (result is not null && !string.IsNullOrEmpty(typeFilter))
                    {
                        result = result.Where(c => c.TypeId.Contains(typeFilter, StringComparison.CurrentCultureIgnoreCase));
                    }
                    if (result is not null && !string.IsNullOrEmpty(subTypeFilter))
                    {
                        result = result.Where(c => c.SubTypeId.Contains(subTypeFilter, StringComparison.CurrentCultureIgnoreCase));
                    }

                    return result;
                }
                return new List<KeenNPCStoreEntry>().AsQueryable();
            }
        }

        public async Task Changed(int index)
        {
            await RunScript();
        }

        public async Task RunScript()
        {
            await JSRuntime.InvokeVoidAsync("setSlotClasses");

        }

        private string typeFilter;
        private string subTypeFilter;
        protected override async Task OnInitializedAsync()
        {
            _storeLogsService.DataRefreshed += async (Guid id) => { await LoadData(id); };

        }
        protected override async Task OnAfterRenderAsync(bool firstRender = false)
        {
            await RunScript();
        }

        public async Task LoadData(Guid serverId)
        {
            if (serverId == ServerId)
            {
                var data = _storeLogsService.GetKeenStoreEntries(ServerId);
                UnfilteredData = data.AsQueryable();
                await InvokeAsync(StateHasChanged);
            }

            await JSRuntime.InvokeVoidAsync("setSlotClasses");
        }

        private void HandleTypeFilter(ChangeEventArgs args)
        {
            if (args.Value is string value)
            {
                typeFilter = value;
            }
        }
        private void HandleSubTypeFilter(ChangeEventArgs args)
        {
            if (args.Value is string value)
            {
                subTypeFilter = value;
            }
        }

        private void HandleClear()
        {
            if (!string.IsNullOrWhiteSpace(typeFilter))
            {
                typeFilter = string.Empty;
            }
        }

        private void HandleSubtypeClear()
        {
            if (!string.IsNullOrWhiteSpace(subTypeFilter))
            {
                subTypeFilter = string.Empty;
            }
        }
    }
}
