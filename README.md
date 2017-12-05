<h1> DeDuplicator </h1>
<h2> A Proof of Concept That Demonstrates Various Hashing Functions</h2>

<p> Among numerous optimizations and cosmetic changes, the bulk of the work is in Hashing/PerceptualHashes.cs. Contained are functions for: </p>
<ul> 
<li> MD5 </li>
<li> Binary Perceptual Hashing </li>
<li> Image Perceptual Hashing: Average Hash(16, 64 Bytes) </li>
<li> Image Perceptual Hashing: Gradient Hash(16, 64 Bytes) </li>
</ul>
<p> Binary perceptual hashes are a gimmicky, yet surprisingly effective way to find matches for corrupted or intentionally mangled files. We read in the binary stream of the file, identify the average value of the characters, average the character values in a series of blocks, and then convert those to an array in a similar method to average hash. Beats MD5 in all cases involving bit flips or minor changes(i.e. compilation time, padding values, corruption).</p>
<p> Average hashes compress and grey scale an image, then finds the average value for all pixels. Using that average value, a value is placed in an array: 1 if the value of the pixel is greater than the average, 0 if it is less. This binary array is then converted to a string and returned. </p>

<p> Gradient hashed compress and grey scale as well, but instead of finding the average, the function checks each pixel value to the one to the left. If the value is greater it is a 1, if it is lower it is a 0. A string is returned in a similar manner to Average Hash. </p>

<p> Both functions come in a 16 and 64 byte length version. 16 is better for discovery, while 64 bytes are better for eliminating false positives as the resolution is greater. Gradient hashes have a lower false positive rate than average hashes, but do not find as many matches. </p>

<p> Currently, the functions are best when added to a dictionary, but I am in the process of re-writing a few functions in order to allow very fuzzy matching for triage purposes. </p>
