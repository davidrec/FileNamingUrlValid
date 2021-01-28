# Validate URL file naming


## The problem: 

E-commerce site include a lot of images with spaces in the file name. 

Gmail Can't show those images sent inside html img tag.  
URL cannot contain spaces, those need to be encoded as + or %20 [RFC 1738](https://tools.ietf.org/html/rfc1738#section-2.2)

> Unsafe:
>   Characters can be unsafe for a number of reasons.  
>   The space character is unsafe because significant spaces may disappear andinsignificant spaces may be introduced when URLs are transcribed or typeset or subjected to the treatment of word-processing programs.


## The solution:

Simple program to change all images and update the DB.
