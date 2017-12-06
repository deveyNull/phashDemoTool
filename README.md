# Perceptual Hashing DeDuplicator Demo
### Proof of Concept That Demonstrates Various Fuzzy Hashing Functions

Credit for the UI and backend goes out to www.codeproject.com/Articles/28512/Duplicate-Files-Finder by eRRaTuM. All I did was integrate alternate functions from [jforshee](https://github.com/jforshee/ImageHashing) for identifying duplicate files and make some minor cosmetic and performance modifications. 

Contained are functions for: 

* MD5
    * MD5 is a cryptographic hash, meaning that it does all sorts of fancy math and provides a unique hash value for each sequence of bytes it is run against. If there are any changes in a file, it will return an entirely different hash.
* Binary Perceptual Hashing 
    * Binary perceptual hashes are a gimmicky, yet surprisingly effective way to find matches for corrupted or intentionally mangled files. To get a BPH we read in the binary stream of the file, identify the average value of the characters, average the character values in a series of blocks, and then convert those to an array in a similar method to average hash. It beats MD5 in all cases involving bit flips or minor changes(i.e. compilation time, padding values, corruption).
* Image Perceptual Hashing: Average Hash(16 & 64 Bytes) 
    * Average hashes compress and greyscale an image, then finds the average value for all pixels. Using that average value, a value is placed in an array: 1 if the value of the pixel is greater than the average, 0 if it is less. This binary array is then converted to a string and returned.
* Image Perceptual Hashing: Gradient Hash(16 & 64 Bytes) 
    * Gradient hashes compress and greyscale an image as well, but instead of finding the average, the function compares each pixel value to the one to the left. If the value is greater it is a 1, if it is lower it is a 0. A string is returned in a similar manner to Average Hash. </p>

Both functions come in a 16 and 64 byte length version. 16 is better for discovery, while 64 bytes are better for eliminating false positives as the resolution is greater. Gradient hashes have a lower false positive rate than average hashes, but do not find as many matches. If you make the shape of a gradient hash more complex the entropy will be higher and the false positive rate will decrease significantly.

