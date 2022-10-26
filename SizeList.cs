using System.Security.Policy;

namespace MOBZize
{
  /// <summary>
  /// Base class for security information for a file or directory
  /// </summary>
  internal class SizeItem
  {
    // Base name of this item (file.ext)
    public string Name { get; init; }
    // Full name of this item (drive:\path\file.ext)
    public string FullName { get; init; }
    // The relative path of the item. "." for the root item
    public string RelativePath { get; init; }
    public Exception? Exception { get; protected set; }

    // The size of the item in bytes
    public long SizeInBytes { get; protected set; }

    protected SizeItem(string fullPath, string rootPath)
    {
      Name = Path.GetFileName(fullPath);
      FullName = fullPath;
      RelativePath = Path.GetRelativePath(rootPath, fullPath);
      SizeInBytes = 0;
    }
  }

  /// <summary>
  /// Security info for a file
  /// </summary>
  internal class SizeFile : SizeItem
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
  /// Security info for a directory and subdirectories
  /// </summary>
  internal class SizeDirectory : SizeItem
  {
    public List<SizeDirectory> Directories { get; init; } = new();
    public List<SizeFile> Files { get; init; } = new();

    protected SizeDirectory(string fullPath, string rootPath, int maxDepth, int currentDepth, Func<string, bool> callback) :
      base(fullPath, rootPath)
    {
      try
      {
        // Get all files and add their sizes:
        foreach (var file in Directory.GetFiles(fullPath))
        {
          var newFile = new SizeFile(file, rootPath);
          Files.Add(newFile);
          SizeInBytes += newFile.SizeInBytes;
        }

        if (maxDepth == 0 || currentDepth < maxDepth)
        {
          foreach (var name in Directory.GetDirectories(fullPath))
          {
            // Do the callback to see if we should cancel
            // (and notify the UI of this directory)
            if (callback(name!))
              break;
            // Not cancelled? Then recurse here:
            var newDir = new SizeDirectory(name, rootPath, maxDepth, currentDepth + 1, callback);
            Directories.Add(newDir);
            // Add it to our size
            SizeInBytes += newDir.SizeInBytes;
          }
        }
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
    /// <param name="depth">0 = recursive, 1 = path only, 2+ = more levels of children</param>
    /// <param name="callback">A function to call upon entering each directory. Can return true to cancel the operation</param>
    /// <returns>The AclDirectory of the root path. Contains all other (files and) directories</returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static SizeDirectory FromPath(string path, int depth, Func<string, bool> callback)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException($"Directory '{path}' does not exist");

      var rootPath = Path.GetFullPath(path);
      var rootItem = new SizeDirectory(rootPath, rootPath, depth, 1, callback);
      return rootItem;
    }
  }
}
