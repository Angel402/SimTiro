using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting.Dependencies.Sqlite;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine.UI;


namespace Utilities
{
    public class LocalDatabase : MonoBehaviour
    {
        private SQLiteConnection db;
        private string[] extensions = {"image files", "png,jpg,jpeg"};
        private byte[] _image;
        private string _selectedOption;
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private TMP_InputField nameInputField;

        private List<string> _rangesList = new()
        {
            "Soldado",
            "Cabo",
            "Sargento segundo",
            "Sargento primero",
            "Subteniente",
            "Teniente",
            "Capitán segundo",
            "Capitán primero",
            "Mayor",
            "Teniente coronel",
            "Coronel",
            "General brigadier",
            "General de brigada",
            "General de división",
            "General secretario de la defensa nacional"
        };


        [Table("Items")]
        public class Item
        {
            [PrimaryKey, AutoIncrement] public int Id { get; set; }
            public string Name { get; set; }
            public string Range { get; set; }
            public byte[] ImageData { get; set; }
        }

        void Start()
        {
            db = new SQLiteConnection("myDatabase.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            db.CreateTable<Item>();
            dropdown.ClearOptions();
            dropdown.AddOptions(_rangesList);
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        
        
        public void InsertItem()
        {
            if (nameInputField.text == "" || _image == null)
            {
                Debug.Log("Es Necesario llenar todos los campos");
                return;
            }
            var newItem = new Item {Name = nameInputField.text, Range = _selectedOption, ImageData = _image};
            db.Insert(newItem);
        }

        
        public void GetAllItems()
        {
            Debug.Log(db.Table<Item>().ToList().Count);
            foreach (var item in db.Table<Item>().ToList())
            {
                Debug.Log(item.Id);
                Debug.Log(item.Name);
                Debug.Log(item.Range);
                Debug.Log(item.ImageData);
            }

             
        }

        public Item GetItemById(int itemId)
        {
            return db.Table<Item>().Where(x => x.Id == itemId).FirstOrDefault();
        }

        
        public void UpdateItem(Item updatedItem)
        {
            db.Update(updatedItem);
        }

        
        public void DeleteItem(int itemId)
        {
            db.Delete<Item>(itemId);
        }
        
        public void ReadImage()
        {
            var path = EditorUtility.OpenFilePanelWithFilters("Selecciona Una Imagen", "", extensions);
            if (!File.Exists(path)) return;
            _image = File.ReadAllBytes(path);
        }
        
        private void OnDropdownValueChanged(int value)
        {
            // Maneja la opción seleccionada
            _selectedOption = _rangesList[value];
            Debug.Log("Selected Option: " + _selectedOption);
        }
    }
}