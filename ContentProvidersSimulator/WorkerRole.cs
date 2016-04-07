using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

using ContentProviderDefinitions;
using ContentProvidersSimulator.ContentProvider;

using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator
{
    public class WorkerRole : RoleEntryPoint
    {
        List<IContentProvider> _contentProviders;

        #region Run()

        public override void Run()
        {
            while( true )
            {
                Thread.Sleep( 5000 );

                #region Connect to Azure Table and see what is in there ...

                var searchesOffersTable = new SearchesOffersTable();

                var searchesTable = new SearchesTable();

                var searchEntities = searchesTable.GetSearches();
                
                #endregion

                foreach( ISearchEntity searchEntity in searchEntities )
                {
                    foreach( IContentProvider contentProvider in _contentProviders )
                    {
                        var searchResult = contentProvider.ProcessSearch( searchEntity ) as SearchResultEntity;

                        if( searchResult != null )
                        {
                            bool canAdd = true;

                            var offersById = searchesOffersTable.GetOffersByProviderId( searchResult.ProviderId );
                            if( offersById != null )
                            {
                                foreach( SearchResultEntity searchResultEntity in offersById )
                                {
                                    if( searchResultEntity.SearchId == searchEntity.Id )
                                    {
                                        canAdd = false;

                                        break;
                                    }
                                }
                            }

                            if( canAdd )
                            {
                                searchResult.SearchId = searchEntity.Id;

                                searchesOffersTable.AddEntity( searchResult );
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region OnStart

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            SearchesTable.CreateIfNotExist();
            SearchesOffersTable.CreateIfNotExist();
            ContentProvidersTable.CreateIfNotExist();

            _contentProviders = new List<IContentProvider>();
            _contentProviders.Add( new Car() );
            _contentProviders.Add( new CarSedan() );
            _contentProviders.Add( new CarTruck() );
            _contentProviders.Add( new CarUsed() );
            _contentProviders.Add( new Piano() );
            _contentProviders.Add( new PianoNew() );
            _contentProviders.Add( new PianoOld() );
            _contentProviders.Add( new PianoChildren() );

            var contentProvidersTable = new ContentProvidersTable();

            foreach( IContentProvider contentProvider in _contentProviders )
            {
                contentProvidersTable.AddEntity( new ContentProviderEntity( contentProvider.GetType().ToString() ) );
            }

            return base.OnStart();
        }

        #endregion
    }
}
