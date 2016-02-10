using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public class CompStrings {

    public readonly String from;
    public String to;
    public String replace;
    public String tolerance;
    public String comp;
    public String col;

    public CompStrings(SqlString from, SqlString to, SqlString replace, SqlString tolerance, SqlString comp) {
        this.from = from.ToString();
        this.to = to.ToString();
        this.replace = replace.ToString();
        this.tolerance = tolerance.ToString();
        this.comp = comp.ToString();
    }

    public CompStrings(SqlString from, SqlString to) {
        this.from = from.ToString();
        this.to = to.ToString();
    }

    public CompStrings(SqlString from, SqlString to, SqlString replace) {
        this.from = from.ToString();
        this.to = to.ToString();
        this.replace = replace.ToString();
    }

    public CompStrings(SqlString from, SqlString to, SqlString tolerance, SqlString comp) {
        this.from = from.ToString();
        this.to = to.ToString();
        this.tolerance = tolerance.ToString();
        this.comp = comp.ToString();
    }

    public CompStrings(SqlString column) {
        this.col = column.ToString();
    }

}

