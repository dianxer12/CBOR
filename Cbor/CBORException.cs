/*
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://upokecenter.com/d/
 */
using System;

namespace PeterO.Cbor {
    /// <summary>Exception thrown for errors involving CBOR data.</summary>
  public class CBORException : Exception {
    /// <summary>Initializes a new instance of the CBORException class.</summary>
    public CBORException() {
    }

    /// <summary>Initializes a new instance of the CBORException class.</summary>
    /// <param name='message'>A string object.</param>
    public CBORException(string message) :
      base(message) {
    }

    /// <summary>Initializes a new instance of the CBORException class. Uses the
    /// given message and inner exception.</summary>
    /// <param name='message'>A string object.</param>
    /// <param name='innerException'>An Exception object.</param>
    public CBORException(string message, Exception innerException) :
      base(message, innerException) {
    }
  }
}
