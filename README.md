# Perceptual Hashing DeDuplicator Demo
### Proof of Concept That Demonstrates Various Fuzzy Hashing Functions

Credit for the UI and backend goes out to www.codeproject.com/Articles/28512/Duplicate-Files-Finder by eRRaTuM. I integrated alternate functions from [jforshee](https://github.com/jforshee/ImageHashing) for identifying duplicate files, built the binary perceptual hashing functions, and made some minor cosmetic and performance modifications. 

![alt text](https://github.com/deveyNull/phashDemoTool/blob/master/DuplicateFinder/demoDemoPic.png "Picture of the tool")

This tool is a very functional program that can be used to find duplicate files on a computer, but is really an introduction to the various types of perceptual hashes that I made as a demonstration while I was an indentured servant for DoD Cyber Crime. Perceptual hashes are a "fingerprint" of a file which reflect what that file looks like so that similar files have similar hashes. This differs from cryptographic hashing which returns entirely different hashes if a file has a single change. Perceptual hashing's robustness against minor changes allows it to be applied to any use cases where a similar, but not exact match needs to be found and various functions work better for different use cases, and this tool is meant to demonstrate them. If you are in the market for a python perceptual hashing library that works at scale, my more noteworthy work in the field is located [here](https://github.com/deveyNull/phistOfFury).

Contained are functions for: 

* MD5 Hashing
    * MD5 is a cryptographic hash, meaning that it does all sorts of fancy math and provides a unique hash value for each sequence of bytes it is run against. If there are any changes in a file, it will return an entirely different hash. 
* Binary Perceptual Hashing 
    * Binary perceptual hashes are a gimmicky, yet surprisingly effective way to find matches for corrupted or intentionally mangled files. To get a BPH we read in the binary stream of the file, identify the average value of the characters, average the character values in a series of blocks, and then convert those to an array in a similar method to average hash. It beats MD5 in all cases involving bit flips or minor changes(i.e. compilation time, padding values, corruption).
* Image Perceptual Hashing: Average Hash(16 & 64 Bytes) 
    * Average hashes compress and greyscale an image, then finds the average value for all pixels. Using that average value, a value is placed in an array: 1 if the value of the pixel is greater than the average, 0 if it is less. This binary array is then converted to a string and returned.
* Image Perceptual Hashing: Gradient Hash(16 & 64 Bytes) 
    * Gradient hashes compress and greyscale an image as well, but instead of finding the average, the function compares each pixel value to the one to the left. If the value is greater it is a 1, if it is lower it is a 0. A string is returned in a similar manner to Average Hash. </p>

Average hash and gradient hashing are file format agnostic, resolution-invariant, scale-invariant, and color-invariant. Both functions come in a 16 and 64 byte length version. 16 is better for discovery, while 64 bytes are better for eliminating false positives as the resolution is greater. Gradient hashes have a lower false positive rate than average hashes, but do not find as many matches. If you make the shape of a gradient hash more complex than a simple left to right the entropy will be higher and the false positive rate will decrease significantly. 

### Instructions: 
1. Download the tool from https://github.com/deveyNull/phashDemoTool/blob/master/precompiledTool.zip?raw=true or compile from source. 
2. Unzip the tool and run setup.exe.

![alt text](https://github.com/deveyNull/phashDemoTool/blob/master/DuplicateFinder/example.JPG "Tool usage")

3. After the tool opens, load the path to the directory 'files' that can be found in the zip.
4. Go to the bar that is labeled "\*.jpg" and change it to "\*.\*". This will cause the tool to search for all matches across all file formats. 
5. Click Go and the tool will run, finding all matches and displaying them below. Double click the file names in order to open them. 
6. In order to change the hashing method, click the button for whatever type you would like in the top right.

**Note:** While I usually like to have the right license, I'm not in the mood to figure out where [CPOL license](https://www.codeproject.com/info/cpol10.aspx) + [GPL](https://www.gnu.org/licenses/gpl-3.0.en.html) + written on .gov computer + while not a paid employee falls so leave me alone internet police. Thanks eRRaTuM and jforshee.
