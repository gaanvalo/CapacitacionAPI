using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PersonasDTO.DTOs
{
    [DataContract]
    public partial class RespuestaList : IEquatable<RespuestaList>
    {
        /// <summary>
        /// Identificador del proceso
        /// </summary>
        /// <value>Identificador del proceso</value>
        [DataMember(Name = "statusCode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Descripción del proceso
        /// </summary>
        /// <value>Descripción del proceso</value>
        [DataMember(Name = "statusDesc")]
        public string StatusDesc { get; set; }

        /// <summary>
        /// Gets or Sets ListPersonas
        /// </summary>
        [DataMember(Name = "ListPersonas")]
        public List<PersonaItem> ListPersonas { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RespuestaList {\n");
            sb.Append("  StatusCode: ").Append(StatusCode).Append("\n");
            sb.Append("  StatusDesc: ").Append(StatusDesc).Append("\n");
            sb.Append("  ListPersonas: ").Append(ListPersonas).Append("\n");
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
            return obj.GetType() == GetType() && Equals((RespuestaList)obj);
        }

        /// <summary>
        /// Returns true if RespuestaList instances are equal
        /// </summary>
        /// <param name="other">Instance of RespuestaList to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RespuestaList other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    StatusCode == other.StatusCode ||
                    StatusCode != null &&
                    StatusCode.Equals(other.StatusCode)
                ) &&
                (
                    StatusDesc == other.StatusDesc ||
                    StatusDesc != null &&
                    StatusDesc.Equals(other.StatusDesc)
                ) &&
                (
                    ListPersonas == other.ListPersonas ||
                    ListPersonas != null &&
                    ListPersonas.SequenceEqual(other.ListPersonas)
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
                if (StatusCode != null)
                    hashCode = hashCode * 59 + StatusCode.GetHashCode();
                if (StatusDesc != null)
                    hashCode = hashCode * 59 + StatusDesc.GetHashCode();
                if (ListPersonas != null)
                    hashCode = hashCode * 59 + ListPersonas.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(RespuestaList left, RespuestaList right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RespuestaList left, RespuestaList right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
