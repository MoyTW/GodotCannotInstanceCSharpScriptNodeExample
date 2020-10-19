using Godot;
using System;

public class SaveLoadButton : Button {
  [Export]
  private string _scenePrefabPath;
  private string _fileLocation;

  public override void _Ready() {
    _fileLocation = string.Format("user://{0}.save", this.Text.Replace(' ', '_'));
  }

  private Node2D Load() {
    Godot.File savefile = new Godot.File();
    if(!savefile.FileExists(_fileLocation)) { throw new NotImplementedException(); }

    savefile.Open(_fileLocation, Godot.File.ModeFlags.Read);
    var scene = savefile.GetVar(true);
    savefile.Close();

    return scene as Node2D;
  }

  private void Save(Node2D node) {
    Godot.File savefile = new Godot.File();
    if(savefile.FileExists(_fileLocation)) { throw new NotImplementedException(); }

    savefile.Open(_fileLocation, Godot.File.ModeFlags.Write);
    savefile.StoreVar(node, true);
    savefile.Close();
  }

  private void Delete() {
    var directory = new Directory();
    directory.Remove(_fileLocation);
  }

  private void OnSaveLoadButtonPressed() {
    var scene = GD.Load<PackedScene>(_scenePrefabPath).Instance() as Node2D;
    Save(scene);
    var node = Load();
    GD.Print(string.Format("Returned node from {0} is {1}", this.Text, node));

    GD.Print("    Adding child node " + node);
    AddChild(node);
    RemoveChild(node);
    GD.Print("    Removing child node " + node);

    Delete();
  }
}
