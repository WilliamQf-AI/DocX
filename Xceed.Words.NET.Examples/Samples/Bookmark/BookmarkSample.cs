﻿/***************************************************************************************
 
   DocX – DocX is the community edition of Xceed Words for .NET
 
   Copyright (C) 2009-2025 Xceed Software Inc.
 
   This program is provided to you under the terms of the XCEED SOFTWARE, INC.
   COMMUNITY LICENSE AGREEMENT (for non-commercial use) as published at 
   https://github.com/xceedsoftware/DocX/blob/master/license.md
 
   For more features and fast professional support,
   pick up Xceed Words for .NET at https://xceed.com/xceed-words-for-net/
 
  *************************************************************************************/


/***************************************************************************************
Xceed Words for .NET – Xceed.Words.NET.Examples – Bookmark Sample Application
Copyright (c) 2009-2025 - Xceed Software Inc.

This application demonstrates how to create or replace a bookmark when using the API 
from the Xceed Words for .NET.

This file is part of Xceed Words for .NET. The source code in this file 
is only intended as a supplement to the documentation, and is provided 
"as is", without warranty of any kind, either expressed or implied.
*************************************************************************************/

using System;
using System.IO;
using System.Linq;
using Xceed.Document.NET;

namespace Xceed.Words.NET.Examples
{
  public class BookmarkSample
  {
    #region Private Members

    private const string BookmarkSampleResourcesDirectory = Program.SampleDirectory + @"Bookmark\Resources\";
    private const string BookmarkSampleOutputDirectory = Program.SampleDirectory + @"Bookmark\Output\";

    #endregion

    #region Constructors

    static BookmarkSample()
    {
      if( !Directory.Exists( BookmarkSample.BookmarkSampleOutputDirectory ) )
      {
        Directory.CreateDirectory( BookmarkSample.BookmarkSampleOutputDirectory );
      }
    }

    #endregion

    #region Public Methods

    public static void InsertBookmarks()
    {
      Console.WriteLine( "\tInsertBookmarks()" );

      // Create a document
      using( var document = DocX.Create( BookmarkSample.BookmarkSampleOutputDirectory + @"InsertBookmarks.docx" ) )
      {
        // Add a title
        document.InsertParagraph( "Insert Bookmarks" ).FontSize( 15d ).SpacingAfter( 40d ).Alignment = Alignment.center;

        // Insert a bookmark in the document.
        document.InsertBookmark( "Bookmark1" );

        // Add a paragraph
        var p = document.InsertParagraph( "This document contains a bookmark named \"" );
        p.Append( document.Bookmarks.First().Name );
        p.Append( "\" just before this line." );
        p.SpacingAfter( 50d );

        var _bookmarkName = "Bookmark2";
        var _displayedBookmarkName = "special";

        // Add another paragraph.
        var p2 = document.InsertParagraph( "This paragraph contains a " );
        // Add a bookmark into the paragraph.
        p2.AppendBookmark( _bookmarkName );
        p2.Append( " bookmark named \"" );
        p2.Append( document.Bookmarks.Last().Name );
        p2.Append( "\" but displayed as \"" + _displayedBookmarkName + "\"." );

        // Inserts the provided text at a bookmark location in this Paragraph.
        p2.InsertAtBookmark( _displayedBookmarkName, _bookmarkName );

        document.Save();
        Console.WriteLine( "\tCreated: InsertBookmarks.docx\n" );
      }
    }

    public static void ReplaceText()
    {
      Console.WriteLine( "\tReplaceBookmarkText()" );

      // Load a document
      using( var document = DocX.Load( BookmarkSample.BookmarkSampleResourcesDirectory + @"DocumentWithBookmarks.docx" ) )
      {
        // Get the regular bookmark from the document and replace its Text.
        var regularBookmark = document.Bookmarks[ "regBookmark" ];
        if( regularBookmark != null )
        {
          regularBookmark.SetText( "Regular Bookmark has been changed" );
        }

        // Get the formatted bookmark from the document and replace its Text.
        var formattedBookmark = document.Bookmarks[ "formattedBookmark" ];
        if( (formattedBookmark != null) 
          && (formattedBookmark.Paragraph != null)
          && (formattedBookmark.Paragraph.MagicText.Count >= 1) )
        {
          formattedBookmark.SetText( "Formatted Bookmark has been changed", formattedBookmark.Paragraph.MagicText[ 1 ].formatting );
        }

        document.SaveAs( BookmarkSample.BookmarkSampleOutputDirectory + @"ReplaceBookmarkText.docx" );
        Console.WriteLine( "\tCreated: ReplaceBookmarkText.docx\n" );
      }
    }

    #endregion
  }
}
