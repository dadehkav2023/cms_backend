using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.Menu;
using Domain.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MenuComposite
{
    [MenuAttribute]
    [AuditableAttribute]
    public abstract class MenuComponent : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoot { get; set; } = false;

        public abstract string print();
        public abstract void Add(MenuComponent menuComponent);
        public abstract void Remove(MenuComponent menuComponent);
    }

    [MenuAttribute]
    [AuditableAttribute]
    public class Menu : MenuComponent
    {
        readonly List<MenuComponent> _menuComponent = new List<MenuComponent>();
        public ICollection<MenuComponent> MenuItem => _menuComponent;
        public Menu(string title, string iconPath, bool isActive)
        {
            Title = title;
            IconPath = iconPath;
            IsActive = isActive;
        }
        public Menu() { }
        public override string print()
        {
            throw new NotImplementedException();
        }
        public override void Add(MenuComponent menuComponent)
        {
            _menuComponent.Add(menuComponent);
        }
        public override void Remove(MenuComponent menuComponent)
        {
            _menuComponent.Remove(menuComponent);
        }

        public object Include()
        {
            throw new NotImplementedException();
        }
    }

    [MenuAttribute]
    [AuditableAttribute]
    public class MenuItem : MenuComponent
    {
        public string CKEditorText { get; set; }
        public string Link { get; set; }
        public string FilePath { get; set; }
        public ServiceTypeEnum? ModularPage { get; set; }

        public MenuItem(string title, string iconPath, bool isActive, string cKEditorText, string link, string filePath, ServiceTypeEnum? modularPage)
        {
            Title = title;
            IconPath = iconPath;
            IsActive = isActive;
            CKEditorText = cKEditorText;
            Link = link;
            FilePath = filePath;
            ModularPage = modularPage;
        }
        public MenuItem() { }
        public override string print()
        {
            return "";
        }
        public override void Add(MenuComponent menuComponent) => throw new NotImplementedException();
        public override void Remove(MenuComponent menuComponent) => throw new NotImplementedException();
    }



}
