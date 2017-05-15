using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain
{

    // Dummy entity, just for testing
    public class FooBar
    {
        public int FooBarId { get; set; }

        public string StringValue { get; set; }
        public int IntValue { get; set; }

        public int WibbleId { get; set; }
        [ForeignKey(name: nameof(WibbleId))]
        public MultiLangString Wibble { get; set; }

        public int WobbleId { get; set; }
        [ForeignKey(name: nameof(WobbleId))]
        public MultiLangString Wobble { get; set; }


        // no Display attribute, no custom messages
        [DataType(dataType: DataType.DateTime)]
        public DateTime DateTime { get; set; }

        // not translated attributes
        [DataType(dataType: DataType.Date, ErrorMessage = "there is an error in  {0} field!")]
        [Display(Name = "KalaMajaDate")]
        public DateTime Date { get; set; }

        // attributes with translation
        [DataType(dataType: DataType.Time, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "FieldMustBeDataTypeDateTime")]
        [Display(ResourceType = typeof(Resources.Misc), Name = "PersonLabel")]
        public DateTime Time { get; set; }  

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        // any name can be used here - EF conventions cannot match several navigvation properties
        public int BlahOneId { get; set; }
        // show which properety is used as FK for this navigation property
        [ForeignKey(name: nameof(BlahOneId))]
        public Blah BlahOne { get; set; }

        public int BlahTwoId { get; set; }
        [ForeignKey(name: nameof(BlahTwoId))]
        public Blah BlahTwo { get; set; }

        public int BlahThreeId { get; set; }
        [ForeignKey(name: nameof(BlahThreeId))]
        public Blah BlahThree { get; set; }
    }
}
