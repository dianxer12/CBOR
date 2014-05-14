/*
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://upokecenter.com/d/
 */
using System;
using System.Collections.Generic;

namespace PeterO {
    /// <summary>Red-black tree, modified by Peter O. from public-domain
    /// Java code originally written by Doug Lea.</summary>
    /// <typeparam name='T'>Type of each element in the tree.</typeparam>
  internal sealed class RedBlackTree<T> : ICollection<T> {
    private sealed class RBCell {
      private const bool RED = false;
      private const bool BLACK = true;

    /// <summary>The element held in the node.</summary>
      private T elementValue;

    /// <summary>The node color (RED, BLACK).</summary>
      private bool colorValue = BLACK;

    /// <summary>Pointer to left child.</summary>
      private RBCell leftValue = null;

    /// <summary>Pointer to right child.</summary>
      private RBCell rightValue = null;

    /// <summary>Pointer to parent (null if root).</summary>
      private RBCell parentValue = null;

    /// <summary>Initializes a new instance of the RBCell class. Make a new
    /// cell with given element, null links, and BLACK color. Normally only
    /// called to establish a new root.</summary>
    /// <param name='element'>A T object.</param>
      public RBCell(T element) {
        this.elementValue = element;
      }

    /// <summary>Return the element value.</summary>
    /// <returns>A T object.</returns>
      public T element() {
        return this.elementValue;
      }

    /// <summary>Set the element value.</summary>
    /// <param name='v'>A T object.</param>
      public void element(T v) {
        this.elementValue = v;
      }

    /// <summary>Return left child (or null).</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell left() {
        return this.leftValue;
      }

    /// <summary>Return right child (or null).</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell right() {
        return this.rightValue;
      }

    /// <summary>Return parent (or null).</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell parent() {
        return this.parentValue;
      }

    /// <summary>Return color of node p, or BLACK if p is null.</summary>
    /// <returns>A Boolean object.</returns>
    /// <param name='p'>A RBCell object.</param>
      internal static bool colorOf(RBCell p) {
        return (p == null) ? BLACK : p.colorValue;
      }

    /// <summary>Return parent of node p, or null if p is null.</summary>
    /// <returns>A RBCell object.</returns>
    /// <param name='p'>A RBCell object. (2).</param>
      internal static RBCell parentOf(RBCell p) {
        return (p == null) ? null : p.parentValue;
      }

    /// <summary>Set the color of node p, or do nothing if p is null.</summary>
    /// <param name='p'>A RBCell object.</param>
    /// <param name='c'>A Boolean object.</param>
      internal static void setColor(RBCell p, bool c) { if (p != null) {
          p.colorValue = c;
        } }

    /// <summary>Return left child of node p, or null if p is null.</summary>
    /// <returns>A RBCell object.</returns>
    /// <param name='p'>A RBCell object. (2).</param>
      internal static RBCell leftOf(RBCell p) {
        return (p == null) ? null : p.leftValue;
      }

    /// <summary>Return right child of node p, or null if p is null.</summary>
    /// <returns>A RBCell object.</returns>
    /// <param name='p'>A RBCell object. (2).</param>
      internal static RBCell rightOf(RBCell p) {
        return (p == null) ? null : p.rightValue;
      }

    /// <summary>Copy all content fields from another node.</summary>
    /// <param name='t'>A RBCell object.</param>
      private void copyContents(RBCell t) {
        this.elementValue = t.elementValue;
      }

    /// <summary>Return the minimum element of the current (sub)tree.</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell leftmost() {
        RBCell p = this;
        for (; p.leftValue != null; p = p.leftValue) {
        }
        return p;
      }

    /// <summary>Return the maximum element of the current (sub)tree.</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell rightmost() {
        RBCell p = this;
        for (; p.rightValue != null; p = p.rightValue) {
        }
        return p;
      }

    /// <summary>Return the root (parentless node) of the tree.</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell root() {
        RBCell p = this;
        for (; p.parentValue != null; p = p.parentValue) {
        }
        return p;
      }

    /// <summary>Return true if node is a root (i.e., has a null parent).</summary>
    /// <returns>A Boolean object.</returns>
      public bool isRoot() {
        return this.parentValue == null;
      }

    /// <summary>Return the in-order successor, or null if no such.</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell successor() {
        if (this.rightValue != null) {
          return this.rightValue.leftmost();
        } else {
          RBCell p = this.parentValue;
          RBCell ch = this;
          while (p != null && ch == p.rightValue) {
            { ch = p;
            } p = p.parentValue; }
          return p;
        }
      }

    /// <summary>Return the in-order predecessor, or null if no such.</summary>
    /// <returns>A RBCell object.</returns>
      public RBCell predecessor() {
        if (this.leftValue != null) {
          return this.leftValue.rightmost();
        } else {
          RBCell p = this.parentValue;
          RBCell ch = this;
          while (p != null && ch == p.leftValue) {
            { ch = p;
            } p = p.parentValue; }
          return p;
        }
      }

    /// <summary>Return the number of nodes in the sub-tree.</summary>
    /// <returns>A 32-bit signed integer.</returns>
      public int size() {
        int c = 1;
        if (this.leftValue != null) {
          c += this.leftValue.size();
        }
        if (this.rightValue != null) {
          c += this.rightValue.size();
        }
        return c;
      }

    /// <summary>Return node of current sub-tree containing element as
    /// element(), if it exists, else null. Uses IComparer <paramref name='cmp'/>
    /// to find and to check equality.</summary>
    /// <returns>A RBCell object.</returns>
    /// <param name='element'>A T object.</param>
    /// <param name='cmp'>An IComparer object.</param>
      public RBCell find(T element, IComparer<T> cmp) {
        RBCell t = this;
        for (;;) {
          int diff = cmp.Compare(element, t.element());
          if (diff == 0) {
            return t;
          } else if (diff < 0) {
            t = t.leftValue;
          } else {
            t = t.rightValue;
          }
          if (t == null) {
            return null;
          }
        }
      }

    /// <summary>Return number of nodes of current sub-tree containing
    /// element. Uses IComparer <paramref name='cmp'/> to find and to check
    /// equality.</summary>
    /// <returns>A 32-bit signed integer.</returns>
    /// <param name='element'>A T object.</param>
    /// <param name='cmp'>An IComparer object.</param>
      public int count(T element, IComparer<T> cmp) {
        int c = 0;
        RBCell t = this;
        while (t != null) {
          int diff = cmp.Compare(element, t.element());
          if (diff == 0) {
            ++c;
            if (t.leftValue == null) {
              t = t.rightValue;
            } else if (t.rightValue == null) {
              t = t.leftValue;
            } else {
              c += t.rightValue.count(element, cmp);
              t = t.leftValue;
            }
          } else if (diff < 0) {
            t = t.leftValue;
          } else {
            t = t.rightValue;
          }
        }
        return c;
      }

    /// <summary>Insert cell as the left child of current node, and then rebalance
    /// the tree it is in. @return the new root of the current tree. (Rebalancing
    /// can change the root!).</summary>
    /// <returns>A RBCell object.</returns>
    /// <param name='cell'>The cell to add.</param>
    /// <param name='root'>Root, the root of the current tree.</param>
      public RBCell insertLeft(RBCell cell, RBCell root) {
        this.leftValue = cell;
        cell.parentValue = this;
        return cell.fixAfterInsertion(root);
      }

    /// <summary>Insert cell as the right child of current node, and then
    /// rebalance the tree it is in.</summary>
    /// <returns>The new root of the current tree. (Rebalancing can change
    /// the root!).</returns>
    /// <param name='cell'>The cell to add.</param>
    /// <param name='root'>The root of the current tree.</param>
      public RBCell insertRight(RBCell cell, RBCell root) {
        this.rightValue = cell;
        cell.parentValue = this;
        return cell.fixAfterInsertion(root);
      }

    /// <summary>Delete the current node, and then rebalance the tree it
    /// is in.</summary>
    /// <param name='root'>The root of the current tree.</param>
    /// <returns>The new root of the current tree. Rebalancing can change
    /// the root.</returns>
      public RBCell delete(RBCell root) {
        // if strictly internal, swap contents with successor and then delete it
        if (this.leftValue != null && this.rightValue != null) {
          RBCell s = this.successor();
          this.copyContents(s);
          return s.delete(root);
        }

        // Start fixup at replacement node, if it exists
        RBCell replacement = (this.leftValue != null) ? this.leftValue : this.rightValue;

        if (replacement != null) {
          // link replacement to parent
          replacement.parentValue = this.parentValue;
          if (this.parentValue == null) {
            root = replacement;
          } else if (this == this.parentValue.leftValue) {
            this.parentValue.leftValue = replacement;
          } else {
            this.parentValue.rightValue = replacement;
          }

          // null out links so they are OK to use by fixAfterDeletion
          this.leftValue = null;
          this.rightValue = null;
          this.parentValue = null;

          // fix replacement
          if (this.colorValue == BLACK) {
            root = replacement.fixAfterDeletion(root);
          }
          return root;
        } else if (this.parentValue == null) {  // exit if we are the only node
          return null;
        } else {  // if no children, use self as phantom replacement and then unlink

          if (this.colorValue == BLACK) {
            root = this.fixAfterDeletion(root);
          }

          // Unlink (Couldn't before since fixAfterDeletion needs parent ptr)
          if (this.parentValue != null) {
            if (this == this.parentValue.leftValue) {
              this.parentValue.leftValue = null;
            } else if (this == this.parentValue.rightValue) {
              this.parentValue.rightValue = null;
            }
            this.parentValue = null;
          }

          return root;
        }
      }

      /** From CLR **/
      private RBCell rotateLeft(RBCell root) {
        RBCell r = this.rightValue;
        this.rightValue = r.leftValue;
        if (r.leftValue != null) {
          r.leftValue.parentValue = this;
        }
        r.parentValue = this.parentValue;
        if (this.parentValue == null) {
          root = r;
        } else if (this.parentValue.leftValue == this) {
          this.parentValue.leftValue = r;
        } else {
          this.parentValue.rightValue = r;
        }
        r.leftValue = this;
        this.parentValue = r;
        return root;
      }

      /** From CLR **/
      private RBCell rotateRight(RBCell root) {
        RBCell l = this.leftValue;
        this.leftValue = l.rightValue;
        if (l.rightValue != null) {
          l.rightValue.parentValue = this;
        }
        l.parentValue = this.parentValue;
        if (this.parentValue == null) {
          root = l;
        } else if (this.parentValue.rightValue == this) {
          this.parentValue.rightValue = l;
        } else {
          this.parentValue.leftValue = l;
        }
        l.rightValue = this;
        this.parentValue = l;
        return root;
      }

      /** From CLR **/
      private RBCell fixAfterInsertion(RBCell root) {
        this.colorValue = RED;
        RBCell x = this;

        while (x != null && x != root && x.parentValue.colorValue == RED) {
          if (parentOf(x) == leftOf(parentOf(parentOf(x)))) {
            RBCell y = rightOf(parentOf(parentOf(x)));

            if (colorOf(y) == RED) {
              setColor(parentOf(x), BLACK);
              setColor(y, BLACK);
              setColor(parentOf(parentOf(x)), RED);
              x = parentOf(parentOf(x));
            } else {
              if (x == rightOf(parentOf(x))) {
                x = parentOf(x);
                root = x.rotateLeft(root);
              }
              setColor(parentOf(x), BLACK);
              setColor(parentOf(parentOf(x)), RED);
              if (parentOf(parentOf(x)) != null) {
                root = parentOf(parentOf(x)).rotateRight(root);
              }
            }
          } else {
            RBCell y = leftOf(parentOf(parentOf(x)));

            if (colorOf(y) == RED) {
              setColor(parentOf(x), BLACK);
              setColor(y, BLACK);
              setColor(parentOf(parentOf(x)), RED);
              x = parentOf(parentOf(x));
            } else {
              if (x == leftOf(parentOf(x))) {
                x = parentOf(x);
                root = x.rotateRight(root);
              }
              setColor(parentOf(x), BLACK);
              setColor(parentOf(parentOf(x)), RED);
              if (parentOf(parentOf(x)) != null) {
                root = parentOf(parentOf(x)).rotateLeft(root);
              }
            }
          }
        }
        root.colorValue = BLACK;
        return root;
      }
      /** From CLR **/
      private RBCell fixAfterDeletion(RBCell root) {
        RBCell x = this;
        while (x != root && colorOf(x) == BLACK) {
          if (x == leftOf(parentOf(x))) {
            RBCell sib = rightOf(parentOf(x));

            if (colorOf(sib) == RED) {
              setColor(sib, BLACK);
              setColor(parentOf(x), RED);
              root = parentOf(x).rotateLeft(root);
              sib = rightOf(parentOf(x));
            }

            if (colorOf(leftOf(sib)) == BLACK && colorOf(rightOf(sib)) == BLACK) {
              setColor(sib, RED);
              x = parentOf(x);
            } else {
              if (colorOf(rightOf(sib)) == BLACK) {
                setColor(leftOf(sib), BLACK);
                setColor(sib, RED);
                root = sib.rotateRight(root);
                sib = rightOf(parentOf(x));
              }
              setColor(sib, colorOf(parentOf(x)));
              setColor(parentOf(x), BLACK);
              setColor(rightOf(sib), BLACK);
              root = parentOf(x).rotateLeft(root);
              x = root;
            }
          } else {  // symmetric

            RBCell sib = leftOf(parentOf(x));

            if (colorOf(sib) == RED) {
              setColor(sib, BLACK);
              setColor(parentOf(x), RED);
              root = parentOf(x).rotateRight(root);
              sib = leftOf(parentOf(x));
            }

            if (colorOf(rightOf(sib)) == BLACK && colorOf(leftOf(sib)) == BLACK) {
              setColor(sib, RED);
              x = parentOf(x);
            } else {
              if (colorOf(leftOf(sib)) == BLACK) {
                setColor(rightOf(sib), BLACK);
                setColor(sib, RED);
                root = sib.rotateLeft(root);
                sib = leftOf(parentOf(x));
              }
              setColor(sib, colorOf(parentOf(x)));
              setColor(parentOf(x), BLACK);
              setColor(leftOf(sib), BLACK);
              root = parentOf(x).rotateRight(root);
              x = root;
            }
          }
        }
        setColor(x, BLACK);
        return root;
      }
    }

    // instance variables

    /// <summary>The root of the tree. Null if empty.</summary>
    private RBCell treeValue;

    /// <summary>The comparator to use for ordering.</summary>
    private IComparer<T> cmpValue;

    // constructors

    /// <summary>Initializes a new instance of the RedBlackTree class.
    /// Make an empty tree. Initialize to use DefaultIComparer for ordering.</summary>
    public RedBlackTree() : this(null, null, 0) {
    }

    /// <summary>Initializes a new instance of the RedBlackTree class.
    /// Make an empty tree, using the supplied element comparator for ordering.</summary>
    /// <param name='c'>An IComparer object.</param>
    public RedBlackTree(IComparer<T> c) : this(c, null, 0) {
    }

    /// <summary>Initializes a new instance of the RedBlackTree class.
    /// Special version of constructor needed by clone().</summary>
    /// <param name='cmp'>An IComparer object.</param>
    /// <param name='t'>A RBCell object.</param>
    /// <param name='n'>A 32-bit signed integer.</param>
    private RedBlackTree(IComparer<T> cmp, RBCell t, int n) {
      this.countValue = n;
      this.treeValue = t;
      if (cmp != null) {
        this.cmpValue = cmp;
      } else {
        this.cmpValue = Comparer<T>.Default;
      }
    }

    // Collection methods

    /// <summary>Implements collections.Collection.includes. Time
    /// complexity: O(log n).</summary>
    /// <returns>A Boolean object.</returns>
    /// <param name='element'>A T object.</param>
    public bool Contains(T element) {
      if (this.countValue == 0) {
        return false;
      }
      return this.treeValue.find(element, this.cmpValue) != null;
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='element'>A T object.</param>
    /// <param name='outval'>A T object. (2).</param>
    /// <returns>A Boolean object.</returns>
    public bool Find(T element, out T outval) {
      if (this.countValue == 0) {
        outval = default(T);
        return false;
      }
      RBCell cell = this.treeValue.find(element, this.cmpValue);
      if (cell == null) {
        outval = default(T);
        return false;
      }
      outval = cell.element();
      return true;
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='element'>A T object.</param>
    /// <returns>A 32-bit signed integer.</returns>
    public int OccurrencesOf(T element) {
      if (this.countValue == 0) {
        return 0;
      }
      return this.treeValue.count(element, this.cmpValue);
    }

    /// <summary>Implements collections.UpdatableCollection.clear.
    /// Time complexity: O(1). @see collections.UpdatableCollection#clear.</summary>
    public void Clear() {
      this.countValue = 0;
      this.treeValue = null;
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='element'>A T object.</param>
    public void RemoveAll(T element) {
      this.remove_(element, true);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='element'>A T object.</param>
    /// <returns>A Boolean object.</returns>
    public bool Remove(T element) {
      return this.remove_(element, false);
    }

    /// <summary>Implements collections.UpdatableCollection.take.
    /// Time complexity: O(log n). Takes the least element. @see collections.UpdatableCollection#take.</summary>
    /// <returns>A T object.</returns>
    public T Pop() {
      if (this.countValue != 0) {
        RBCell p = this.treeValue.leftmost();
        T v = p.element();
        this.treeValue = p.delete(this.treeValue);
        this.decCount();
        return v;
      }
      return default(T);
    }

    private enum OccurrenceMode {
    /// <summary>Always add the element even if it exists.</summary>
      AlwaysAdd,

    /// <summary>Add the element only if it exists.</summary>
      AddIfMissing,

    /// <summary>Add the element and remove the existing element if any.</summary>
      OverwriteIfExisting
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='element'>A T object.</param>
    /// <returns>A Boolean object.</returns>
    public bool AddIfMissing(T element) {
      return this.addInternal(element, OccurrenceMode.AddIfMissing);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='element'>A T object.</param>
    /// <returns>A Boolean object.</returns>
    public bool AddOverwrite(T element) {
      return this.addInternal(element, OccurrenceMode.OverwriteIfExisting);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='element'>A T object.</param>
    public void Add(T element) {
      this.addInternal(element, OccurrenceMode.AlwaysAdd);
    }

    // helper methods
    private bool addInternal(T element, OccurrenceMode checkOccurrence) {
      if (this.treeValue == null) {
        this.treeValue = new RBCell(element);
        this.incCount();
      } else {
        RBCell t = this.treeValue;
        for (;;) {
          int diff = this.cmpValue.Compare(element, t.element());
          if (diff == 0 && checkOccurrence == OccurrenceMode.AddIfMissing) {
            return false;
          }
          if (diff == 0 && checkOccurrence == OccurrenceMode.OverwriteIfExisting) {
            t.element(element);
            return false;
          } else if (diff <= 0) {
            if (t.left() != null) {
              t = t.left();
            } else {
              this.treeValue = t.insertLeft(new RBCell(element), this.treeValue);
              this.incCount();
              return true;
            }
          } else {
            if (t.right() != null) {
              t = t.right();
            } else {
              this.treeValue = t.insertRight(new RBCell(element), this.treeValue);
              this.incCount();
              return true;
            }
          }
        }
      }
      return true;
    }

    private bool remove_(T element, bool allOccurrences) {
      bool ret = false;
      while (this.countValue > 0) {
        RBCell p = this.treeValue.find(element, this.cmpValue);
        if (p != null) {
          this.treeValue = p.delete(this.treeValue);
          this.decCount();
          ret = true;
          if (!allOccurrences) {
            return ret;
          }
        } else {
          break;
        }
      }
      return ret;
    }

    private IEnumerable<T> Iterator() {
      if (this.treeValue != null) {
        RBCell t = this.treeValue.leftmost();
        while (t != null) {
          T v = t.element();
          yield return v;
          t = t.successor();
        }
      }
    }

    private int countValue;

    private void incCount() {
      ++this.countValue;
    }

    private void decCount() {
      --this.countValue;
    }

    /// <summary>Gets a value not documented yet.</summary>
    /// <value>A value not documented yet.</value>
    public int Count {
      get {
        return this.countValue;
      }
    }

    /// <summary>Gets a value indicating whether this map is read-only.</summary>
    /// <value>Always false.</value>
    public bool IsReadOnly {
      get {
        return false;
      }
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='array'>A T[] object.</param>
    /// <param name='arrayIndex'>A 32-bit signed integer.</param>
    public void CopyTo(T[] array, int arrayIndex) {
      if (this.treeValue != null) {
        RBCell t = this.treeValue.leftmost();
        while (t != null && arrayIndex < array.Length) {
          T v = t.element();
          if (arrayIndex >= 0 && arrayIndex < array.Length) {
            array[arrayIndex] = v;
          }
          ++arrayIndex;
          t = t.successor();
        }
      }
    }

    /// <summary>Not documented yet.</summary>
    /// <returns>An IEnumerator(T) object.</returns>
    public IEnumerator<T> GetEnumerator() {
      return this.Iterator().GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
      return this.Iterator().GetEnumerator();
    }
  }
}
