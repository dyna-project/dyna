﻿using System;
using System.Diagnostics.Contracts;

namespace Workbench.Services
{
    /// <summary>
    /// Manage the single instance of the workspace document.
    /// </summary>
    public class DocumentManager : IDocumentManager
    {
        private IWorkspaceDocument _currentDocument;
        private readonly IViewModelFactory _viewModelFactory;

        /// <summary>
        /// Initialize a new document manager with a view model factory.
        /// </summary>
        /// <param name="theViewModelFactory">View model factory.</param>
        public DocumentManager(IViewModelFactory theViewModelFactory)
        {
            Contract.Requires<ArgumentNullException>(theViewModelFactory != null);
            _viewModelFactory = theViewModelFactory;
        }

        /// <summary>
        /// Gets or sets the current document.
        /// </summary>
        public IWorkspaceDocument CurrentDocument
        {
            get => _currentDocument;
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                _currentDocument = value;
            }
        }

        /// <summary>
        /// Create a new document.
        /// </summary>
        public IWorkspaceDocument CreateDocument()
        {
            CurrentDocument = _viewModelFactory.CreateDocument();

            return CurrentDocument;
        }
    }
}
