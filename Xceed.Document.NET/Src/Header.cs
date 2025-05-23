﻿/***************************************************************************************
 
   DocX – DocX is the community edition of Xceed Words for .NET
 
   Copyright (C) 2009-2025 Xceed Software Inc.
 
   This program is provided to you under the terms of the XCEED SOFTWARE, INC.
   COMMUNITY LICENSE AGREEMENT (for non-commercial use) as published at 
   https://github.com/xceedsoftware/DocX/blob/master/license.md
 
   For more features and fast professional support,
   pick up Xceed Words for .NET at https://xceed.com/xceed-words-for-net/
 
  *************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO.Packaging;
using System.Collections.ObjectModel;

namespace Xceed.Document.NET
{
  public class Header : Container, IParagraphContainer
  {

    #region Public Properties

    public Paragraph PageNumberParagraph
    {
      get;
      set;
    }

    public bool PageNumbers
    {
      get
      {
        return false;
      }

      set
      {
        XElement e = XElement.Parse
        ( @"<w:sdt xmlns:w='http://schemas.openxmlformats.org/wordprocessingml/2006/main'>
                    <w:sdtPr>
                      <w:id w:val='157571950' />
                      <w:docPartObj>
                        <w:docPartGallery w:val='Page Numbers (Top of Page)' />
                        <w:docPartUnique />
                      </w:docPartObj>
                    </w:sdtPr>
                    <w:sdtContent>
                      <w:p w:rsidR='008D2BFB' w:rsidRDefault='008D2BFB'>
                        <w:pPr>
                          <w:pStyle w:val='Header' />
                          <w:jc w:val='center' />
                        </w:pPr>
                        <w:fldSimple w:instr=' PAGE \* MERGEFORMAT'>
                          <w:r>
                            <w:rPr>
                              <w:noProof />
                            </w:rPr>
                            <w:t>1</w:t>
                          </w:r>
                        </w:fldSimple>
                      </w:p>
                    </w:sdtContent>
                  </w:sdt>"
       );

        Xml.AddFirst( e );

        this.PageNumberParagraph = new Paragraph( this.Document, e.Descendants( XName.Get( "p", Document.w.NamespaceName ) ).SingleOrDefault(), 0 );
      }
    }

    public override ReadOnlyCollection<Paragraph> Paragraphs
    {
      get
      {
        var paragraphs = base.Paragraphs;
        foreach( var paragraph in paragraphs )
        {
          paragraph.PackagePart = this.PackagePart;
        }
        return paragraphs;
      }
    }

    public override List<Table> Tables
    {
      get
      {
        var l = base.Tables;
        l.ForEach( x => x.PackagePart = this.PackagePart );
        return l;
      }
    }

    public List<Image> Images
    {
      get
      {
        var imageRelationships = this.PackagePart.GetRelationshipsByType( Document.RelationshipImage );
        if( imageRelationships.Count() > 0 )
        {
          return
          (
              from i in imageRelationships
              select new Image( Document, i )
          ).ToList();
        }

        return new List<Image>();
      }
    }

    #endregion

    #region Internal Properties

    internal string Id
    {
      get;
      private set;
    }

    #endregion

    #region Constructors

    internal Header( Document document, XElement xml, PackagePart mainPart, string id ) : base( document, xml )
    {
      this.PackagePart = mainPart;
      this.Id = id;
    }

    #endregion

    #region Public Methods

    public override Paragraph InsertParagraph()
    {
      var p = base.InsertParagraph();
      p.PackagePart = this.PackagePart;
      return p;
    }

    public override Paragraph InsertParagraph( int index, string text, bool trackChanges )
    {
      var p = base.InsertParagraph( index, text, trackChanges );
      p.PackagePart = this.PackagePart;
      return p;
    }

    public override Paragraph InsertParagraph( Paragraph p )
    {
      p.PackagePart = this.PackagePart;
      return base.InsertParagraph( p );
    }

    public override Paragraph InsertParagraph( int index, Paragraph p )
    {
      p.PackagePart = this.PackagePart;
      return base.InsertParagraph( index, p );
    }

    public override Paragraph InsertParagraph( int index, string text, bool trackChanges, Formatting formatting )
    {
      var p = base.InsertParagraph( index, text, trackChanges, formatting );
      p.PackagePart = this.PackagePart;
      return p;
    }

    public override Paragraph InsertParagraph( string text )
    {
      Paragraph p = null;

      if( this.Paragraphs.Count == 0 )
      {
        p = base.InsertParagraph( text );
        p.StyleId = "header";
      }
      else
      {
        p = base.InsertParagraph( text );
      }

      p.PackagePart = this.PackagePart;

      return p;
    }

    public override Paragraph InsertParagraph( string text, bool trackChanges )
    {
      var p = base.InsertParagraph( text, trackChanges );
      p.PackagePart = this.PackagePart;
      return p;
    }

    public override Paragraph InsertParagraph( string text, bool trackChanges, Formatting formatting )
    {
      var p = base.InsertParagraph( text, trackChanges, formatting );
      p.PackagePart = this.PackagePart;

      return p;
    }

    public override Paragraph InsertEquation( String equation, Alignment align = Alignment.center )
    {
      var p = base.InsertEquation( equation, align );
      p.PackagePart = this.PackagePart;
      return p;
    }

    public override Table InsertTable( int rowCount, int columnCount )
    {
      var table = base.InsertTable( rowCount, columnCount );
      return this.SetMainPart( table );
    }

    public override Table InsertTable( int index, Table t )
    {
      var table = base.InsertTable( index, t );
      return this.SetMainPart( table );
    }

    public override Table InsertTable( Table t )
    {
      var table = base.InsertTable( t );
      return this.SetMainPart( table );
    }

    public override Table InsertTable( int index, int rowCount, int columnCount )
    {
      var table = base.InsertTable( index, rowCount, columnCount );
      return this.SetMainPart( table );
    }

    #endregion

    #region Private Methods

    private Table SetMainPart( Table table )
    {
      if( table != null )
      {
        table.PackagePart = this.PackagePart;
      }
      return table;
    }

    #endregion
  }
}
