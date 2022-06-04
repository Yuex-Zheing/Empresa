/*
 * Cuentas
 *
 * Operaciones con Cuentas
 *
 * OpenAPI spec version: 1.0
 * Contact: wquimis@gmail.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace EmpresaAPI.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class MovimientoReporte : IEquatable<MovimientoReporte>
    { 
        /// <summary>
        /// Gets or Sets Fecha
        /// </summary>

        [DataMember(Name="fecha")]
        public DateTime? Fecha { get; set; }

        /// <summary>
        /// Gets or Sets Cliente
        /// </summary>

        [DataMember(Name="cliente")]
        public string Cliente { get; set; }

        /// <summary>
        /// Gets or Sets NumeroCuenta
        /// </summary>

        [DataMember(Name="numeroCuenta")]
        public string NumeroCuenta { get; set; }

        /// <summary>
        /// Gets or Sets TipoCuenta
        /// </summary>

        [DataMember(Name="tipoCuenta")]
        public string TipoCuenta { get; set; }

        /// <summary>
        /// Gets or Sets SaldoInicial
        /// </summary>

        [DataMember(Name="saldoInicial")]
        public double? SaldoInicial { get; set; }

        /// <summary>
        /// Gets or Sets EstadoMovimiento
        /// </summary>

        [DataMember(Name="estadoMovimiento")]
        public string EstadoMovimiento { get; set; }

        /// <summary>
        /// Gets or Sets Movimiento
        /// </summary>

        [DataMember(Name="movimiento")]
        public double? Movimiento { get; set; }

        /// <summary>
        /// Gets or Sets SaldoDisponible
        /// </summary>

        [DataMember(Name="saldoDisponible")]
        public double? SaldoDisponible { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MovimientoReporte {\n");
            sb.Append("  Fecha: ").Append(Fecha).Append("\n");
            sb.Append("  Cliente: ").Append(Cliente).Append("\n");
            sb.Append("  NumeroCuenta: ").Append(NumeroCuenta).Append("\n");
            sb.Append("  TipoCuenta: ").Append(TipoCuenta).Append("\n");
            sb.Append("  SaldoInicial: ").Append(SaldoInicial).Append("\n");
            sb.Append("  EstadoMovimiento: ").Append(EstadoMovimiento).Append("\n");
            sb.Append("  Movimiento: ").Append(Movimiento).Append("\n");
            sb.Append("  SaldoDisponible: ").Append(SaldoDisponible).Append("\n");
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
            return obj.GetType() == GetType() && Equals((MovimientoReporte)obj);
        }

        /// <summary>
        /// Returns true if MovimientoReporte instances are equal
        /// </summary>
        /// <param name="other">Instance of MovimientoReporte to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MovimientoReporte other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Fecha == other.Fecha ||
                    Fecha != null &&
                    Fecha.Equals(other.Fecha)
                ) && 
                (
                    Cliente == other.Cliente ||
                    Cliente != null &&
                    Cliente.Equals(other.Cliente)
                ) && 
                (
                    NumeroCuenta == other.NumeroCuenta ||
                    NumeroCuenta != null &&
                    NumeroCuenta.Equals(other.NumeroCuenta)
                ) && 
                (
                    TipoCuenta == other.TipoCuenta ||
                    TipoCuenta != null &&
                    TipoCuenta.Equals(other.TipoCuenta)
                ) && 
                (
                    SaldoInicial == other.SaldoInicial ||
                    SaldoInicial != null &&
                    SaldoInicial.Equals(other.SaldoInicial)
                ) && 
                (
                    EstadoMovimiento == other.EstadoMovimiento ||
                    EstadoMovimiento != null &&
                    EstadoMovimiento.Equals(other.EstadoMovimiento)
                ) && 
                (
                    Movimiento == other.Movimiento ||
                    Movimiento != null &&
                    Movimiento.Equals(other.Movimiento)
                ) && 
                (
                    SaldoDisponible == other.SaldoDisponible ||
                    SaldoDisponible != null &&
                    SaldoDisponible.Equals(other.SaldoDisponible)
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
                    if (Fecha != null)
                    hashCode = hashCode * 59 + Fecha.GetHashCode();
                    if (Cliente != null)
                    hashCode = hashCode * 59 + Cliente.GetHashCode();
                    if (NumeroCuenta != null)
                    hashCode = hashCode * 59 + NumeroCuenta.GetHashCode();
                    if (TipoCuenta != null)
                    hashCode = hashCode * 59 + TipoCuenta.GetHashCode();
                    if (SaldoInicial != null)
                    hashCode = hashCode * 59 + SaldoInicial.GetHashCode();
                    if (EstadoMovimiento != null)
                    hashCode = hashCode * 59 + EstadoMovimiento.GetHashCode();
                    if (Movimiento != null)
                    hashCode = hashCode * 59 + Movimiento.GetHashCode();
                    if (SaldoDisponible != null)
                    hashCode = hashCode * 59 + SaldoDisponible.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(MovimientoReporte left, MovimientoReporte right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MovimientoReporte left, MovimientoReporte right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
