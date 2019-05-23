using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dvt.Features.Messages.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class LinkSystemFunctionGroupRequest
    {
        /// <summary>
        /// Gets or sets the FunctionId property
        /// </summary>
        [Required]
        public int FunctionId { get; set; }

        /// <summary>
        /// Gets or sets the GroupId property
        /// </summary>
        public int GroupId { get; set; }

    }
}
