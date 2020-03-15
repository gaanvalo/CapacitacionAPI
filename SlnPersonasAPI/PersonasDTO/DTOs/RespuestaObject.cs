using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PersonasDTO.DTOs
{
    [DataContract]
    public partial class RespuestaObject : IEquatable<RespuestaObject>
    {
        /// <summary>
        /// Identificador de la consulta
        /// </summary>
        /// <value>Identificador de la consulta</value>
        [DataMember(Name = "statusCode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// descripcion del mensaje
        /// </summary>
        /// <value>descripcion del mensaje</value>
        [DataMember(Name = "statusDesc")]
        public string StatusDesc { get; set; }

        /// <summary>
        /// Gets or Sets Schema
        /// </summary>
        [DataMember(Name = "schema")]
        public PersonaItem Schema { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RespuestaObject {\n");
            sb.Append("  StatusCode: ").Append(StatusCode).Append("\n");
            sb.Append("  StatusDesc: ").Append(StatusDesc).Append("\n");
            sb.Append("  Schema: ").Append(Schema).Append("\n");
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
            return obj.GetType() == GetType() && Equals((RespuestaObject)obj);
        }

        /// <summary>
        /// Returns true if RespuestaObject instances are equal
        /// </summary>
        /// <param name="other">Instance of RespuestaObject to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RespuestaObject other)
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
                    Schema == other.Schema ||
                    Schema != null &&
                    Schema.Equals(other.Schema)
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
                if (Schema != null)
                    hashCode = hashCode * 59 + Schema.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(RespuestaObject left, RespuestaObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RespuestaObject left, RespuestaObject right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
