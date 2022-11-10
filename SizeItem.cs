using System.IO;
using System.Security.Policy;

namespace MOBZize
{
  /// <summary>
  /// Base Immutable record for size and path information for a file or directory
  /// </summary>
  internal record SizeItem(
    string Name, // file.ext
    string FullName, // drive:\path\file.ext
    string RelativePath, // subdir\file.ext 
    long SizeInBytes
  )
  {
    // An optional exception that occurred retrieving the item
    public Exception? Exception { get; protected set; }

    protected SizeItem(string fullPath, string rootPath) :
      this(
        Name: Path.GetFileName(fullPath),
        FullName: fullPath,
        RelativePath: Path.GetRelativePath(rootPath, fullPath),
        SizeInBytes: 0
      )
    { }
  }

  /// <summary>
  /// Size info for a file
  /// </summary>
  internal record SizeFile : SizeItem
  {
    public SizeFile(string fullPath, string rootPath) :
      base(fullPath, rootPath)
    {
      try
      {
        SizeInBytes = new FileInfo(fullPath).Length;
      }
      catch (Exception ex)
      {
        Exception = ex;
      }
    }
  }

  /// <summary>
  /// Size info for a directory and subdirectories
  /// </summary>
  internal record SizeDirectory : SizeItem
  {
    public List<SizeDirectory> Directories { get; init; } = new();
    public List<SizeFile> Files { get; init; } = new();

    public int TotalFileCount { get; init; }
    public int TotalDirectoryCount { get; init; }

    protected SizeDirectory(string fullPath, string rootPath, Func<string, bool> callback) :
      base(fullPath, rootPath)
    {
      TotalFileCount = 0;
      TotalDirectoryCount = 0;

      try
      {
        // Get all files and add their sizes:
        foreach (var file in Directory.GetFiles(fullPath))
        {
          var newFile = new SizeFile(file, rootPath);
          Files.Add(newFile);
          SizeInBytes += newFile.SizeInBytes;
          // Count this file
          TotalFileCount++;
        }

        foreach (var name in Directory.GetDirectories(fullPath))
        {
          // Do the callback to see if we should cancel
          // (and notify the UI of this directory)
          if (callback(name!))
            break;
          // Not cancelled? Then recurse here:
          var newDir = new SizeDirectory(name, rootPath, callback);
          Directories.Add(newDir);
          // Add it to our size and counters
          SizeInBytes += newDir.SizeInBytes;

          TotalFileCount += newDir.TotalFileCount;
          TotalDirectoryCount += newDir.TotalDirectoryCount;
        }

        // Count the directorues themselves
        TotalDirectoryCount += Directories.Count();
      }
      catch (Exception ex)
      {
        // Store the exception to signal the UI that the directory could not be loaded
        Exception = ex;
      }
    }

    /// <summary>
    /// Load security information from a path
    /// </summary>
    /// <param name="path">The (full or relative) 'root' path</param>
    /// <param name="callback">A function to call upon entering each directory. Can return true to cancel the operation</param>
    /// <returns>The AclDirectory of the root path. Contains all other (files and) directories</returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static SizeDirectory FromPath(string path, Func<string, bool> callback)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException($"Directory '{path}' does not exist");

      var rootPath = Path.GetFullPath(path);
      var rootItem = new SizeDirectory(rootPath, rootPath, callback);
      return rootItem;
    }
  }
}
