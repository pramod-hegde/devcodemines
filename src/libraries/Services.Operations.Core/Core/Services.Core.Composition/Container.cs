﻿using Services.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Services.Core.Composition
{
    public sealed class Container
    {
        static Container _instance = null;

        public static Container Default
        {
            get
            {
                if (_instance == null)
                {                    
                    using (var semaphore = new Semaphore(0, 1, Guid.NewGuid().ToString(), out bool createNew))
                    {
                        if (createNew)
                        {
                            _instance = new Container();
                            semaphore.Release();
                        }
                    }
                }

                return _instance;
            }
        }

        CompositionHost _containerHost = null;

        private Container() { }

        [ImportMany(typeof(ICompositionPart), RequiredCreationPolicy = CreationPolicy.NonShared, AllowRecomposition = true, Source = ImportSource.Any)]
        IEnumerable<ICompositionPart> Parts { get; set; }

        public void InitializeParts(params Assembly[] clientAssemblies)
        {
            if (clientAssemblies == null || !clientAssemblies.Any())
            {
                throw new Exception("Container creation failed due to missing arguments");
            }            

            if (_containerHost == null || Parts==null || !Parts.Any())
            {
                using (var semaphore = new Semaphore(0, 1, Guid.NewGuid().ToString(), out bool createNew))
                {
                    if (createNew)
                    {
                        Dispose();
                        CreateContainer(clientAssemblies);
                        semaphore.Release();
                    }
                }                
            }

            Parts = _containerHost.GetExports<ICompositionPart>();
        }

        private void CreateContainer(Assembly[] clientAssemblies)
        {
            var conventions = new ConventionBuilder();
            conventions.ForTypesDerivedFrom<ICompositionPart>().Export<ICompositionPart>();
            var configuration = new ContainerConfiguration().WithAssemblies(clientAssemblies, conventions);
            _containerHost = configuration.CreateContainer();
        }

        // Gets the shared instance of a specific part
        public TOut GetShared<TOut>(string clientId)
        {
            if (Parts==null || !Parts.Any(c => c.Id.Equals(clientId)))
            {
                throw new InvalidPartException($"Invalid part: {clientId}");
            }
            return (TOut)Parts.First(c => c.Id.Equals(clientId));
        }

        // Gets the shared instance of all parts
        public IEnumerable<TOut> GetSharedAll<TOut>()
        {
            if (Parts == null || !Parts.Any())
            {
                throw new InvalidPartException($"No valid composition parts found");
            }

            return Parts.OfType<TOut>();
        }

        //get the non-shared instance
        public TOut Get<TOut>(string clientId)
        {
            if (_containerHost == null) 
            {
                throw new InvalidPartException($"Container is null or empty");
            }

            var parts = _containerHost.GetExports<ICompositionPart>();
            if (parts == null || !parts.Any(p => p.Id.Equals(clientId)))
            {
                throw new InvalidPartException($"Invalid part: {clientId}");
            }

            return (TOut)parts.First(p => p.Id.Equals(clientId));
        }

        //get all the non-shared instance
        public IEnumerable<TOut> GetAll<TOut>()
        {
            if (_containerHost == null)
            {
                throw new InvalidPartException($"Container is null or empty");
            }

            var parts = _containerHost.GetExports<ICompositionPart>();
            if (parts == null || !parts.Any())
            {
                throw new InvalidPartException($"No valid composition parts found");
            }

            return parts.OfType<TOut>();
        }

        public void Dispose()
        {
            if (_containerHost != null)
            {
                _containerHost.Dispose();
                _containerHost = null;
            }

            Parts = null;

            GC.Collect();
        }
    }
}
