//
//  MakeDictionary.m
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "GetData.h"

@implementation GetData

#define JSON_PERSONS @"http://crawler.firstexperience.ru/api/v1/persons/"
#define JSON_SITES @"http://crawler.firstexperience.ru/api/v1/sites/"
//#define JSON_EVERYDAYRATES @"http://crawler.firstexperience.ru/api/v1/rankeveryday/"


//get list of sites from db and put it in array
- (void) getSites {
    
    NSArray *data = [[NSArray alloc] init];
    NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:JSON_SITES]];
    NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
    data = jsonResult;
    NSMutableArray *sitesJSON = [NSMutableArray array];
    NSMutableArray *sitesIDJSON = [NSMutableArray array];
    for (id item in jsonResult) {
        [sitesJSON addObject:[NSString stringWithFormat:@"%@", item[@"sites"]]];
        [sitesIDJSON addObject:[NSString stringWithFormat:@"%@", item[@"id"]]];
    }
    self.sites = sitesJSON;
    self.sitesID = sitesIDJSON;
}

//get list of rates from db and put it in array
- (void) getNames {
    NSArray *data = [[NSArray alloc] init];
    NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:JSON_PERSONS]];
    NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
    data = jsonResult;
    NSMutableArray *namesJSON = [NSMutableArray array];
    for (id item in jsonResult)
        [namesJSON addObject:[NSString stringWithFormat:@"%@", item[@"persons"]]];
    self.names = namesJSON;
}

@end
