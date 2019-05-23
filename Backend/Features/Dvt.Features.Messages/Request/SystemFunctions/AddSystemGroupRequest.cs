using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dvt.Features.Messages.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class AddSystemGroupRequest
    {
        /// <summary>
        /// Gets or sets the DisplayName property
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the Name property
        /// </summary>
        [Required]
        public string Name { get; set; }

    }
}
