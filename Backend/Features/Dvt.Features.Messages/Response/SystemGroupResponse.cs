using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Messages.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemGroupResponse
    {
        /// <summary>
        /// Gets or sets the SystemGroupId property
        /// </summary>
        public int SystemGroupId { get; set; }
        /// <summary>
        /// Gets or sets the DisplayName property
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the Name property
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the SystemFunctions property
        /// </summary>
        public List<string> SystemFunctions { get; set; }


    }
}
