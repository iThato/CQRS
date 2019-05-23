using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dvt.Features.Messages.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSystemGroupByIdRequest
    {
        /// <summary>
        /// Gets or sets the Id property
        /// </summary>
        [Required]
        public int Id { get; set; }

    }
}
