//
//  MakeDictionary.h
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface MakeDictionary : NSObject

@property (strong, nonatomic) NSMutableArray *sites;
@property (strong, nonatomic) NSMutableArray *names;
@property (strong, nonatomic) NSMutableArray *allRates;
@property (strong, nonatomic) NSMutableArray *sitesID;

- (void) makeDictionary;
- (void) getSites;
- (void) getRates;

@end
