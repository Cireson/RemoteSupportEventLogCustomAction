using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Cireson.ControlCenter.Core.Actions.Base;
using Cireson.ControlCenter.Core.RsOperations.Attributes;
using Cireson.Core.Common.Attributes;
using Cireson.ControlCenter.Core.Models.ConfigurationManager;
using Cireson.RemoteManage.PlatformServices.Adapter.Interfaces;
using Newtonsoft.Json;

namespace RemoteSupportEventLogCustomAction.Actions
{
    /// <summary>
    /// Class supports a Custom Action that is bound to CmDevice entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to perform the bound action on.</typeparam>
    /// <example>
    /// POST http://localhost/api/{EntitySet}({EntityId)/Action.GetEventsLog
    /// </example>
    /// <RsOperation>
    /// This attribute indicates where the custom action is going to be placed.
    /// Params ( ActionType , Custom Action Name, Bound Entity Type, Order, Group )
    ///     Action Type: Defines whether this action will be a Remote Action, Quick Action or One Click Action.
    ///     ·Remote actions will be placed under the remote manage flyout for the specified entity type.
    ///     ·Quick actions will be placed under the grid 'more actions' flyout.
    ///     ·One click actions will be placed and available as the grid row actions.
    ///     Custom Action Name: Name for your custom action.
    ///     Bound Entity Type: This is the entity set that the action will be bound to.
    ///     Order: This is the order of your custom action and it is used only for templating purposes.
    ///     Group: There are three default groups for custom actions which are RCA, QA, OCA and go for each type of action accordingly.
    ///     ·RCA: Remote Custom Action.
    ///     ·QA: Quick Action.
    ///     ·OCA: One Click Action.
    /// </RsOperation>
    [ODataAlias]
    [RsOperation(RsOperationAttribute.ActionTypes.Remote, "GetEventLog", "Cireson.ControlCenter.Core.Models.ConfigurationManager.CmDevice", 1, "RCA")]
    [RsOperation(RsOperationAttribute.ActionTypes.Quick, "GetEventLog", "Cireson.ControlCenter.Core.Models.ConfigurationManager.CmDevice", 1, "QA")]
    [RsOperation(RsOperationAttribute.ActionTypes.OneClick, "GetEventLog", "Cireson.ControlCenter.Core.Models.ConfigurationManager.CmDevice", 1, "OCA")]
    public class GetEventLog<TEntity> : BoundActionLoggingBase<TEntity, EventLogResult> where TEntity : CmDevice
    {
        private readonly IPsProcessor _psProcessor;

        public string LogName { get; set; }
        public int Newest { get; set; }

        public GetEventLog(IPsProcessor psProcessor)
        {
            _psProcessor = psProcessor;
        }
        /// <summary>
        /// Asynchronously executes a POST request from an odata client or browser.
        /// </summary>
        /// <returns>Returns the execution result.</returns>
        public override async Task<EventLogResult> ExecuteAsync()
        {
            var eventLogs = await _psProcessor.ExecutePowershell(Entity.DNSName, $"Get-EventLog -LogName {LogName} -Newest {Newest}");

            await ActionLogger.LogActionSuccess(Entity.DNSName, Entity.ResourceId, this, "GetEventLog executed successfully.");

            return new EventLogResult
            {
                EventLogItems = eventLogs.PipelineResults
                    .Select(JsonConvert.DeserializeObject<EventLogItem>).ToList()
            };
        }
    }
    
    public class EventLogItem
    {
        public string EventID { get; set; }
        public string MachineName { get; set; }
        public string Data { get; set; }
        public int Index { get; set; }
        public string Category { get; set; }
        public int CategoryNumber { get; set; }
        public string EntryType { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public long InstanceId { get; set; }
        public string TimeGenerated { get; set; }
        public string TimeWritten { get; set; }
        public string UserName { get; set; }
        public string Site { get; set; }
        public string Container { get; set; }

    }

    public class EventLogResult
    {
        public List<EventLogItem> EventLogItems { get; set; }
    }
}
