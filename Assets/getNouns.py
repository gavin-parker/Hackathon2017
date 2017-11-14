#!/usr/bin/env python
# -*- coding: utf-8 -*-
from textblob import TextBlob
import sys
#text = '''
#The titular threat of The Blob has always struck me as the ultimate movie
#monster: an insatiably hungry, amoeba-like mass able to penetrate
#virtually any safeguard, capable of--as a doomed doctor chillingly
#describes it--"assimilating flesh on contact.
#Snide comparisons to gelatin be damned, it's a concept with the most
#devastating of potential consequences, not unlike the grey goo scenario
#proposed by technological theorists fearful of
#artificial intelligence run rampant.
#'''

def main():
    text = sys.argv[1]
    blob = TextBlob(text)
    blob.tags           # [('The', 'DT'), ('titular', 'JJ'),
                        #  ('threat', 'NN'), ('of', 'IN'), ...]
    
    mynewtext = blob.noun_phrases
    #print(mynewtext)    # WordList(['titular threat', 'blob',
                        #            'ultimate movie monster',
                        #            'amoeba-like mass', ...])
    output = ""
    for word in mynewtext:
        output += word + " "
    print(output)
    return output
    #blob.translate(to="es")  # 'La amenaza titular de The Blob...'

if __name__ == "__main__":
   main()