// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace k8s.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// PodReadinessGate contains the reference to a pod condition
    /// </summary>
    public partial class V1PodReadinessGate
    {
        /// <summary>
        /// Initializes a new instance of the V1PodReadinessGate class.
        /// </summary>
        public V1PodReadinessGate()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the V1PodReadinessGate class.
        /// </summary>
        /// <param name="conditionType">ConditionType refers to a condition in
        /// the pod's condition list with matching type.</param>
        public V1PodReadinessGate(string conditionType)
        {
            ConditionType = conditionType;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets conditionType refers to a condition in the pod's
        /// condition list with matching type.
        /// </summary>
        [JsonProperty(PropertyName = "conditionType")]
        public string ConditionType { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (ConditionType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ConditionType");
            }
        }
    }
}