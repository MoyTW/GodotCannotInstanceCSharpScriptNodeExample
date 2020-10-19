using Godot;
using System;

public class SaveLoadCSharpScriptScene : Button {
  private static PackedScene _scenePrefab = GD.Load<PackedScene>("res://CSharpScriptScene.tscn");

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

  private void OnSaveLoadCSharpScriptScenePressed() {
    var scene = _scenePrefab.Instance() as Node2D;
    Save(scene);
    var node = Load();
    GD.Print("Returned node is: ", node);
    Delete();
  }
}
