using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace PersonasDTO.DTOs
{
    public class PersonaItem: CommonBase
    {
       


        /// <summary>
        /// Campo identificacion de registro
        /// </summary>
        /// <value>Campo identificacion de registro</value>
        [Required]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Primer Nombre
        /// </summary>
        /// <value>Primer Nombre</value>
        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Segundo Nombre
        /// </summary>
        /// <value>Segundo Nombre</value>
        [Required]
        [DataMember(Name = "MiddleName")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Apellido
        /// </summary>
        /// <value>Apellido</value>
        [Required]
        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PersonaItem {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  MiddleName: ").Append(MiddleName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PersonaItem)obj);
        }

        /// <summary>
        /// Returns true if PersonaItem instances are equal
        /// </summary>
        /// <param name="other">Instance of PersonaItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PersonaItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id &&
                    Id.Equals(other.Id)
                ) &&
                (
                    FirstName == other.FirstName ||
                    FirstName != null &&
                    FirstName.Equals(other.FirstName)
                ) &&
                (
                    MiddleName == other.MiddleName ||
                    MiddleName != null &&
                    MiddleName.Equals(other.MiddleName)
                ) &&
                (
                    LastName == other.LastName ||
                    LastName != null &&
                    LastName.Equals(other.LastName)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (FirstName != null)
                    hashCode = hashCode * 59 + FirstName.GetHashCode();
                if (MiddleName != null)
                    hashCode = hashCode * 59 + MiddleName.GetHashCode();
                if (LastName != null)
                    hashCode = hashCode * 59 + LastName.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(PersonaItem left, PersonaItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonaItem left, PersonaItem right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators

    }
}
