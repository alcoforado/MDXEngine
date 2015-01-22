using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace TestApp
{
   
	/// <summary>
	/// Represents a databindable, customized view of two dimentional array
	/// </summary>
	public class Array2DDataView	: IBindingList
	{
		#region Variables

		/// <summary>
		/// array rows.
		/// </summary>
		private		ArrayRowView[]	_rows;
		/// <summary>
		/// Data which will be binded.
		/// </summary>
		internal	Array			_data;
		/// <summary>
		/// Alternative column names.
		/// </summary>
		private		string[]		_colnames;	

		#endregion // Variables

		#region Constructors

		/// <summary>
		/// Initializes a new ArrayDataView from array.
		/// </summary>
		/// <param name="array">array of data.</param>
		public Array2DDataView(Array array)
		{
			if (array.Rank != 2)
				throw new ArgumentException("Supports only two dimentional arrays","array");

			_data = array;
		
			_rows = new ArrayRowView[array.GetLength(0)];

			for(int i = 0; i < _rows.Length; i++)
			{
				_rows[i] = new ArrayRowView(this,i);
			}
		}
		
		/// <summary>
		/// Initializes a new ArrayDataView from array with custom column names.
		/// </summary>
		/// <param name="array">array of data.</param>
		/// <param name="colnames">collection of column names.</param>
		public Array2DDataView(Array array, object[] colnames) : this(array)
		{
			if(colnames.Length != array.GetLength(1))
				throw new ArgumentException("column names must correspond to array columns.","colnames");

			_colnames = new string[colnames.Length];
			for(int i = 0; i < colnames.Length; i++)
			{
				_colnames[i] = colnames[i].ToString();
			}
		}

		#endregion // Constructors

//		#region Events
//		public delegate void ValueChangingEventHandler(int row,int col,object value);
//		public event ValueChangingEventHandler ValueChaning;
//
//		internal void OnValueChaning(int row,int col,object value)
//		{
//			if(
//		}
//		
//
//		#endregion // Events

		#region Properties

		internal string[] ColumnNames
		{
			get
			{
				if (_colnames == null)
				{
					_colnames = new string[_data.GetLength(1)];
					for(int i = 0; i < _colnames.Length; i++)
					{
						_colnames[i] = i.ToString();
					}
				}
				return _colnames;
			}
		}

		#endregion // Properties

		#region Methods

		public void Reset()
		{
			OnListChanged(new ListChangedEventArgs(ListChangedType.Reset,-1));
		}

		#endregion 

		#region IBindingList Members

		public void AddIndex(PropertyDescriptor property)
		{
			// TODO:  Add ArrayDataView.AddIndex implementation
		}

		public bool AllowNew
		{
			get
			{
				// TODO:  Add ArrayDataView.AllowNew getter implementation
				return false;
			}
		}

		public void ApplySort(PropertyDescriptor property, System.ComponentModel.ListSortDirection direction)
		{
			// TODO:  Add ArrayDataView.ApplySort implementation
		}

		public PropertyDescriptor SortProperty
		{
			get
			{
				// TODO:  Add ArrayDataView.SortProperty getter implementation
				return null;
			}
		}

		public int Find(PropertyDescriptor property, object key)
		{
			// TODO:  Add ArrayDataView.Find implementation
			return 0;
		}

		public bool SupportsSorting
		{
			get
			{
				// TODO:  Add ArrayDataView.SupportsSorting getter implementation
				return false;
			}
		}

		public bool IsSorted
		{
			get
			{
				// TODO:  Add ArrayDataView.IsSorted getter implementation
				return false;
			}
		}

		public bool AllowRemove
		{
			get
			{
				// TODO:  Add ArrayDataView.AllowRemove getter implementation
				return false;
			}
		}

		public bool SupportsSearching
		{
			get
			{
				// TODO:  Add ArrayDataView.SupportsSearching getter implementation
				return false;
			}
		}

		public System.ComponentModel.ListSortDirection SortDirection
		{
			get
			{
				// TODO:  Add ArrayDataView.SortDirection getter implementation
				return new System.ComponentModel.ListSortDirection ();
			}
		}

		public event System.ComponentModel.ListChangedEventHandler ListChanged;

		private void OnListChanged(ListChangedEventArgs e)
		{
			if (ListChanged != null)
			{
				ListChanged(this,e);
			}
		}

		public bool SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		public void RemoveSort()
		{
			// TODO:  Add ArrayDataView.RemoveSort implementation
		}

		public object AddNew()
		{
			// TODO:  Add ArrayDataView.AddNew implementation
			return null;
		}

		public bool AllowEdit
		{
			get
			{
				return true;
			}
		}

		public void RemoveIndex(PropertyDescriptor property)
		{
			// TODO:  Add ArrayDataView.RemoveIndex implementation
		}

		#endregion

		#region IList Members

		public bool IsReadOnly
		{
			get
			{
				return true; // we hold array.
			}
		}

		public object this[int index]
		{
			get
			{
				return _rows[index];
			}
			set
			{
				
			}
		}

		public void RemoveAt(int index)
		{
			// TODO:  Add ArrayDataView.RemoveAt implementation
		}

		public void Insert(int index, object value)
		{
			// TODO:  Add ArrayDataView.Insert implementation
		}

		public void Remove(object value)
		{
			// TODO:  Add ArrayDataView.Remove implementation
		}

		public bool Contains(object value)
		{
			// TODO:  Add ArrayDataView.Contains implementation
			return false;
		}

		public void Clear()
		{
			// TODO:  Add ArrayDataView.Clear implementation
		}

		public int IndexOf(object value)
		{
			// TODO:  Add ArrayDataView.IndexOf implementation
			return 0;
		}

		public int Add(object value)
		{
			// TODO:  Add ArrayDataView.Add implementation
			return 0;
		}

		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region ICollection Members

		public bool IsSynchronized
		{
			get
			{
				// TODO:  Add ArrayDataView.IsSynchronized getter implementation
				return false;
			}
		}

		public int Count
		{
			get
			{
				// TODO:  Add ArrayDataView.Count getter implementation
				return _rows.Length;
			}
		}

		public void CopyTo(System.Array array, int index)
		{
			// TODO:  Add ArrayDataView.CopyTo implementation
		}

		public object SyncRoot
		{
			get
			{
				// TODO:  Add ArrayDataView.SyncRoot getter implementation
				return null;
			}
		}

		#endregion

		#region IEnumerable Members

		public System.Collections.IEnumerator GetEnumerator()
		{
			// TODO:  Add ArrayDataView.GetEnumerator implementation
			return _rows.GetEnumerator();
		}

		#endregion
	}// END CLASS DEFINITION ArrayDataView


/// <summary>
	/// Property decriptor for array
	/// </summary>
	public class ArrayPropertyDescriptor	: PropertyDescriptor
	{
		private string		_name;
		private Type		_type;
		private int			_index;

		public ArrayPropertyDescriptor(string name,Type type,int index) : base (name,null)
		{
			_name	= name;
			_type	= type;
			_index  = index;
		}

		public override string DisplayName
		{
			get
			{
				return _name;
			}
		}

		public override Type ComponentType
		{
			get
			{
				return typeof(ArrayRowView);
			}
		}
		
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}
		public override Type PropertyType
		{
			get
			{
				return _type;
			}
		}
		
		public override object GetValue(object component)
		{
			try
			{

				return ((ArrayRowView)component).GetColumn(_index);
			}
			catch(Exception e)
			{
				Debug.WriteLine(e);
			}

			Debug.Assert(false);

			return null;
		}

		public override void SetValue(object component, object value)
		{
			try
			{
				((ArrayRowView)component).SetColumnValue(_index,value);
			}
			catch(Exception e)
			{
				Debug.WriteLine(e);
				Debug.Assert(false);
			}

			
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}
		public override void ResetValue(object component)
		{
			
		}
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}// END CLASS DEFINITION ArrayPropertyDescriptor

	public class ArrayRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo
	{
		/// <summary>
		/// Owner of the row.
		/// </summary>
		private Array2DDataView	_owner;
		private int				_index;

		string					_error;

		internal ArrayRowView(Array2DDataView owner,int index)
		{
			_owner = owner;
			_index = index;
		}

		

		internal object GetColumn(int index)
		{
			return _owner._data.GetValue(_index,index);
		}

		internal void SetColumnValue(int index,object value)
		{
			try
			{
				_owner._data.SetValue(value,_index,index);
			}
			catch(Exception e)
			{
				_error = e.ToString();
			}
		}

		#region ICustomTypeDescriptor Members

		public TypeConverter GetConverter()
		{
			// TODO:  Add ArrayColumn.GetConverter implementation
			return null;
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		public string GetComponentName()
		{
			return null;
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return _owner;
		}

		public AttributeCollection GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			int col = _owner._data.GetLength(1);
			Type type = _owner._data.GetType().GetElementType();
			PropertyDescriptor[] prop = new PropertyDescriptor[col];
			for(int i = 0; i < col; i++)
			{
				prop[i] = new ArrayPropertyDescriptor(_owner.ColumnNames[i],type,i);
			}
			return new PropertyDescriptorCollection(prop);
		}

		PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
		{
			return GetProperties(null);
		}

		public object GetEditor(Type editorBaseType)
		{
			return null;
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		public EventDescriptor GetDefaultEvent()
		{
			return null;
		}

		public string GetClassName()
		{
			return this.GetType().Name;
		}

		#endregion

		#region IEditableObject Members

		public void EndEdit()
		{
			// TODO:  Add ArrayRowView.EndEdit implementation
		}

		public void CancelEdit()
		{
			// TODO:  Add ArrayRowView.CancelEdit implementation
		}

		public void BeginEdit()
		{
			// TODO:  Add ArrayRowView.BeginEdit implementation
		}

		#endregion

		#region IDataErrorInfo Members

		public string this[string columnName]
		{
			get
			{
				// TODO:  Add ArrayRowView.this getter implementation
				return null;
			}
		}

		public string Error
		{
			get
			{
				// TODO:  Add ArrayRowView.Error getter implementation
				return null;
			}
		}

		#endregion
	}// END CLASS DEFINITION ArrayRowView


} 









