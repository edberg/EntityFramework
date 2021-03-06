// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Configures the runtime behavior of warnings generated by Entity Framework. You can set a default behavior and behaviors for
    ///         each warning type.
    ///     </para>
    ///     <para>
    ///         This class is used within the <see cref="DbContextOptionsBuilder.ConfigureWarnings(System.Action{WarningsConfigurationBuilder})" />
    ///         API and it is not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    public class WarningsConfigurationBuilder
    {
        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="WarningsConfigurationBuilder"/> class.
        ///     </para>
        ///     <para>
        ///         This class is used within the <see cref="DbContextOptionsBuilder.ConfigureWarnings(System.Action{WarningsConfigurationBuilder})" />
        ///         API and it is not designed to be directly constructed in your application code.
        ///     </para>
        /// </summary>
        /// <param name="warningsConfiguration"> The internal object used to store configuration. </param>
        public WarningsConfigurationBuilder([CanBeNull] WarningsConfiguration warningsConfiguration)
        {
            Configuration = warningsConfiguration ?? new WarningsConfiguration();
        }

        /// <summary>
        ///     Gets the internal object used to store configuration.
        /// </summary>
        public virtual WarningsConfiguration Configuration { get; }

        /// <summary>
        ///     Sets the default behavior when a warning is generated.
        /// </summary>
        /// <param name="warningBehavior"> The desired behavior. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public virtual WarningsConfigurationBuilder Default(WarningBehavior warningBehavior)
        {
            Configuration.DefaultBehavior = warningBehavior;

            return this;
        }

        /// <summary>
        ///     Causes an exception to be thrown when the specified core warnings are generated. Database providers (and other extensions)
        ///     may provide extension method overloads of this method to configure this behavior for warnings they generate.  
        /// </summary>
        /// <param name="coreEventIds">
        ///     The <see cref="CoreEventId"/>(s) for the warnings.
        /// </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public virtual WarningsConfigurationBuilder Throw(
            [NotNull] params CoreEventId[] coreEventIds)
        {
            Check.NotNull(coreEventIds, nameof(coreEventIds));

            Configuration.AddExplicit(coreEventIds.Cast<object>(), WarningBehavior.Throw);

            return this;
        }

        /// <summary>
        ///     Causes warning to be logged when the specified core warnings are generated. Database providers (and other extensions)
        ///     may provide extension method overloads of this method to configure this behavior for warnings they generate.  
        /// </summary>
        /// <param name="coreEventIds">
        ///     The <see cref="CoreEventId"/>(s) for the warnings.
        /// </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public virtual WarningsConfigurationBuilder Log(
            [NotNull] params CoreEventId[] coreEventIds)
        {
            Check.NotNull(coreEventIds, nameof(coreEventIds));

            Configuration.AddExplicit(coreEventIds.Cast<object>(), WarningBehavior.Log);

            return this;
        }

        /// <summary>
        ///     Causes nothing to happen when the specified core warnings are generated. Database providers (and other extensions)
        ///     may provide extension method overloads of this method to configure this behavior for warnings they generate.  
        /// </summary>
        /// <param name="coreEventIds">
        ///     The <see cref="CoreEventId"/>(s) for the warnings.
        /// </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public virtual WarningsConfigurationBuilder Ignore(
            [NotNull] params CoreEventId[] coreEventIds)
        {
            Check.NotNull(coreEventIds, nameof(coreEventIds));

            Configuration.AddExplicit(coreEventIds.Cast<object>(), WarningBehavior.Ignore);

            return this;
        }
    }
}
