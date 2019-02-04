using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Workbench.Core.Models
{
    /// <summary>
    /// One solution to the model.
    /// </summary>
    [Serializable]
    public sealed class SolutionSnapshot
    {
        private readonly List<SingletonLabelModel> singletonLabels;
        private readonly List<CompoundLabelModel> compoundLabels;
        private readonly List<BucketLabelModel> bucketLabels;
        private TimeSpan duration;

        /// <summary>
        /// Initialize a solution snapshot with singleton labels, compound labels and the solution duration.
        /// </summary>
        public SolutionSnapshot(IEnumerable<SingletonLabelModel> theSingletonLabels, IEnumerable<CompoundLabelModel> theCompoundLabels, TimeSpan theDuration)
        {
            this.singletonLabels = new List<SingletonLabelModel>(theSingletonLabels);
            this.compoundLabels = new List<CompoundLabelModel>(theCompoundLabels);
            this.bucketLabels = new List<BucketLabelModel>();
	        this.duration = theDuration;
        }

        /// <summary>
        /// Initialize a solution snapshot with default values.
        /// </summary>
        public SolutionSnapshot()
        {
            this.singletonLabels = new List<SingletonLabelModel>();
            this.compoundLabels = new List<CompoundLabelModel>();
            this.bucketLabels = new List<BucketLabelModel>();
        }

        /// <summary>
        /// Gets the singleton variable labels.
        /// </summary>
        public IReadOnlyCollection<SingletonLabelModel> SingletonLabels
        {
            get
            {
                Contract.Ensures(Contract.Result<IReadOnlyCollection<SingletonLabelModel>>() != null);
                return this.singletonLabels.ToList();
            }
        }

        /// <summary>
        /// Gets the aggregate variable labels.
        /// </summary>
        public IReadOnlyCollection<CompoundLabelModel> AggregateLabels
        {
            get
            {
                Contract.Ensures(Contract.Result<IReadOnlyCollection<CompoundLabelModel>>() != null);
                return this.compoundLabels.ToList();
            }
        }

        /// <summary>
        /// Gets the bucket labels.
        /// </summary>
        public IReadOnlyCollection<BucketLabelModel> BucketLabels => new ReadOnlyCollection<BucketLabelModel>(this.bucketLabels);

        /// <summary>
        /// Gets the time taken to solve the model.
        /// </summary>
        public TimeSpan Duration
        {
            get { return this.duration; }
            internal set { this.duration = value; }
        }

        /// <summary>
        /// Add a label to the snapshot.
        /// </summary>
        /// <param name="newSingletonLabel">Singleton label.</param>
        internal void AddSingletonLabel(SingletonLabelModel newSingletonLabel)
        {
            Contract.Requires<ArgumentNullException>(newSingletonLabel != null);
            this.singletonLabels.Add(newSingletonLabel);
        }

        /// <summary>
        /// Add a compound label to the snapshot.
        /// </summary>
        /// <param name="newCompoundLabel">Aggregate label.</param>
        internal void AddAggregateLabel(CompoundLabelModel newCompoundLabel)
        {
            Contract.Requires<ArgumentNullException>(newCompoundLabel != null);
            this.compoundLabels.Add(newCompoundLabel);
        }

        /// <summary>
        /// Add bucket label.
        /// </summary>
        /// <param name="bucketLabel">Bucket label.</param>
        internal void AddBucketLabel(BucketLabelModel bucketLabel)
        {
            Contract.Requires<ArgumentNullException>(bucketLabel != null);
            this.bucketLabels.Add(bucketLabel);
        }

        /// <summary>
        /// Get the compound label matching the variable name.
        /// </summary>
        /// <param name="theAggregateVariableName">Aggregate variable name.</param>
        /// <returns>Compound label for the aggregate variable.</returns>
        public CompoundLabelModel GetCompoundLabelByVariableName(string theAggregateVariableName)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(theAggregateVariableName));
            return this.compoundLabels.FirstOrDefault(_ => _.Variable.Name.IsEqualTo(theAggregateVariableName));
        }

        /// <summary>
        /// Get the label matching the variable name.
        /// </summary>
        /// <param name="theSingletonVariableName">Singleton variable name.</param>
        /// <returns>Label for the singleton variable.</returns>
        public SingletonLabelModel GetLabelByVariableName(string theSingletonVariableName)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(theSingletonVariableName));
            return this.singletonLabels.FirstOrDefault(_ => _.VariableName == theSingletonVariableName);
        }
    }
}