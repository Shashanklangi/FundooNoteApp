using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabID { get; set; }
        public string CollabEmail { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }

        [ForeignKey("Notes")]
        public long NoteID { get; set; }

        [JsonIgnore]
        public virtual UserEntity Notes { get; set; }
    }
}
