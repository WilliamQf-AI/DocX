﻿/***************************************************************************************
 
   DocX – DocX is the community edition of Xceed Words for .NET
 
   Copyright (C) 2009-2025 Xceed Software Inc.
 
   This program is provided to you under the terms of the XCEED SOFTWARE, INC.
   COMMUNITY LICENSE AGREEMENT (for non-commercial use) as published at 
   https://github.com/xceedsoftware/DocX/blob/master/license.md
 
   For more features and fast professional support,
   pick up Xceed Words for .NET at https://xceed.com/xceed-words-for-net/
 
  *************************************************************************************/


namespace Xceed.Document.NET
{
  public class Headers
  {
    #region Public Properties

    public Header Odd
    {
      get;
      set;
    }

    public Header Even
    {
      get;
      set;
    }

    public Header First
    {
      get;
      set;
    }

    #endregion

    #region Constructors

    internal Headers()
    {
    }

    #endregion
  }
}
