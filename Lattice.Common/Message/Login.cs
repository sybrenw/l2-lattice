// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: login.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Lattice.Protobuf.Login {

  /// <summary>Holder for reflection information generated from login.proto</summary>
  public static partial class LoginReflection {

    #region Descriptor
    /// <summary>File descriptor for login.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static LoginReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cgtsb2dpbi5wcm90bxIFbG9naW4iNwoOUmVnaXN0ZXJTZXJ2ZXISCgoCaWQY",
            "ASABKAUSDAoEbmFtZRgCIAEoCRILCgNrZXkYAyABKAkiLgoQUmVnaXN0ZXJS",
            "ZXNwb25zZRIKCgJpZBgBIAEoBRIOCgZyZXN1bHQYAiABKAUiYQoRUGxheWVy",
            "QXV0aFJlcXVlc3QSDgoGc2VydmVyGAEgASgFEhEKCWFjY291bnRJZBgCIAEo",
            "BRIUCgxsb2dpbkF1dGhLZXkYAyABKAUSEwoLZ2FtZUF1dGhLZXkYBCABKAUi",
            "NwoSUGxheWVyQXV0aFJlc3BvbnNlEhEKCWFjY291bnRJZBgBIAEoBRIOCgZy",
            "ZXN1bHQYAiABKAVCG6oCGEwyTGF0dGljZS5Qcm90b2J1Zi5Mb2dpbmIGcHJv",
            "dG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Lattice.Protobuf.Login.RegisterServer), global::Lattice.Protobuf.Login.RegisterServer.Parser, new[]{ "Id", "Name", "Key" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Lattice.Protobuf.Login.RegisterResponse), global::Lattice.Protobuf.Login.RegisterResponse.Parser, new[]{ "Id", "Result" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Lattice.Protobuf.Login.PlayerAuthRequest), global::Lattice.Protobuf.Login.PlayerAuthRequest.Parser, new[]{ "Server", "AccountId", "LoginAuthKey", "GameAuthKey" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Lattice.Protobuf.Login.PlayerAuthResponse), global::Lattice.Protobuf.Login.PlayerAuthResponse.Parser, new[]{ "AccountId", "Result" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class RegisterServer : pb::IMessage<RegisterServer> {
    private static readonly pb::MessageParser<RegisterServer> _parser = new pb::MessageParser<RegisterServer>(() => new RegisterServer());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RegisterServer> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Lattice.Protobuf.Login.LoginReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterServer() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterServer(RegisterServer other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      key_ = other.key_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterServer Clone() {
      return new RegisterServer(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "key" field.</summary>
    public const int KeyFieldNumber = 3;
    private string key_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Key {
      get { return key_; }
      set {
        key_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RegisterServer);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RegisterServer other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (Key != other.Key) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Key.Length != 0) hash ^= Key.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Key.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Key);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Key.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Key);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RegisterServer other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Key.Length != 0) {
        Key = other.Key;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            Key = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class RegisterResponse : pb::IMessage<RegisterResponse> {
    private static readonly pb::MessageParser<RegisterResponse> _parser = new pb::MessageParser<RegisterResponse>(() => new RegisterResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RegisterResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Lattice.Protobuf.Login.LoginReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterResponse(RegisterResponse other) : this() {
      id_ = other.id_;
      result_ = other.result_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterResponse Clone() {
      return new RegisterResponse(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 2;
    private int result_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RegisterResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RegisterResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Result != other.Result) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Result != 0) hash ^= Result.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Result != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Result);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RegisterResponse other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 16: {
            Result = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class PlayerAuthRequest : pb::IMessage<PlayerAuthRequest> {
    private static readonly pb::MessageParser<PlayerAuthRequest> _parser = new pb::MessageParser<PlayerAuthRequest>(() => new PlayerAuthRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlayerAuthRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Lattice.Protobuf.Login.LoginReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerAuthRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerAuthRequest(PlayerAuthRequest other) : this() {
      server_ = other.server_;
      accountId_ = other.accountId_;
      loginAuthKey_ = other.loginAuthKey_;
      gameAuthKey_ = other.gameAuthKey_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerAuthRequest Clone() {
      return new PlayerAuthRequest(this);
    }

    /// <summary>Field number for the "server" field.</summary>
    public const int ServerFieldNumber = 1;
    private int server_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Server {
      get { return server_; }
      set {
        server_ = value;
      }
    }

    /// <summary>Field number for the "accountId" field.</summary>
    public const int AccountIdFieldNumber = 2;
    private int accountId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int AccountId {
      get { return accountId_; }
      set {
        accountId_ = value;
      }
    }

    /// <summary>Field number for the "loginAuthKey" field.</summary>
    public const int LoginAuthKeyFieldNumber = 3;
    private int loginAuthKey_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int LoginAuthKey {
      get { return loginAuthKey_; }
      set {
        loginAuthKey_ = value;
      }
    }

    /// <summary>Field number for the "gameAuthKey" field.</summary>
    public const int GameAuthKeyFieldNumber = 4;
    private int gameAuthKey_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GameAuthKey {
      get { return gameAuthKey_; }
      set {
        gameAuthKey_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PlayerAuthRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PlayerAuthRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Server != other.Server) return false;
      if (AccountId != other.AccountId) return false;
      if (LoginAuthKey != other.LoginAuthKey) return false;
      if (GameAuthKey != other.GameAuthKey) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Server != 0) hash ^= Server.GetHashCode();
      if (AccountId != 0) hash ^= AccountId.GetHashCode();
      if (LoginAuthKey != 0) hash ^= LoginAuthKey.GetHashCode();
      if (GameAuthKey != 0) hash ^= GameAuthKey.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Server != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Server);
      }
      if (AccountId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(AccountId);
      }
      if (LoginAuthKey != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(LoginAuthKey);
      }
      if (GameAuthKey != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(GameAuthKey);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Server != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Server);
      }
      if (AccountId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(AccountId);
      }
      if (LoginAuthKey != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(LoginAuthKey);
      }
      if (GameAuthKey != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GameAuthKey);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PlayerAuthRequest other) {
      if (other == null) {
        return;
      }
      if (other.Server != 0) {
        Server = other.Server;
      }
      if (other.AccountId != 0) {
        AccountId = other.AccountId;
      }
      if (other.LoginAuthKey != 0) {
        LoginAuthKey = other.LoginAuthKey;
      }
      if (other.GameAuthKey != 0) {
        GameAuthKey = other.GameAuthKey;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Server = input.ReadInt32();
            break;
          }
          case 16: {
            AccountId = input.ReadInt32();
            break;
          }
          case 24: {
            LoginAuthKey = input.ReadInt32();
            break;
          }
          case 32: {
            GameAuthKey = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class PlayerAuthResponse : pb::IMessage<PlayerAuthResponse> {
    private static readonly pb::MessageParser<PlayerAuthResponse> _parser = new pb::MessageParser<PlayerAuthResponse>(() => new PlayerAuthResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlayerAuthResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Lattice.Protobuf.Login.LoginReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerAuthResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerAuthResponse(PlayerAuthResponse other) : this() {
      accountId_ = other.accountId_;
      result_ = other.result_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlayerAuthResponse Clone() {
      return new PlayerAuthResponse(this);
    }

    /// <summary>Field number for the "accountId" field.</summary>
    public const int AccountIdFieldNumber = 1;
    private int accountId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int AccountId {
      get { return accountId_; }
      set {
        accountId_ = value;
      }
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 2;
    private int result_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PlayerAuthResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PlayerAuthResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AccountId != other.AccountId) return false;
      if (Result != other.Result) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (AccountId != 0) hash ^= AccountId.GetHashCode();
      if (Result != 0) hash ^= Result.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (AccountId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(AccountId);
      }
      if (Result != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Result);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (AccountId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(AccountId);
      }
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PlayerAuthResponse other) {
      if (other == null) {
        return;
      }
      if (other.AccountId != 0) {
        AccountId = other.AccountId;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            AccountId = input.ReadInt32();
            break;
          }
          case 16: {
            Result = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
