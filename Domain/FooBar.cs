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

        [DataType(dataType: DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [DataType(dataType: DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(dataType: DataType.Time)]
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
