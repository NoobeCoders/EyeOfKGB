//
//  GeneralStatisticViewController.m
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "GeneralStatisticViewController.h"
#import "GeneralStatisticTableViewCell.h"
#import "MakeDictionary.h"

@interface GeneralStatisticViewController () <UITableViewDataSource, UITableViewDelegate, UIPickerViewDataSource, UIPickerViewDelegate>

@property (strong, nonatomic) MakeDictionary *data;
@property (weak, nonatomic) IBOutlet UIPickerView *sitePicker;
@property (strong, nonatomic) NSMutableArray *ratesPrev;
@property (strong, nonatomic) NSMutableArray *rates;
@property (weak, nonatomic) IBOutlet UITableView *tableView;

@end

@implementation GeneralStatisticViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.sitePicker.delegate = self;
    self.sitePicker.dataSource = self;
    
    self.data = [[MakeDictionary alloc] init];
    [self.data makeDictionary];
    [self.data getSites];
    [self.data getRates];
    
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

//MARK: - Delegates and data sources (table)

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return 3;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    GeneralStatisticTableViewCell *cell = (GeneralStatisticTableViewCell *) [tableView dequeueReusableCellWithIdentifier:@"Cell" forIndexPath:indexPath];
    
    cell.labelName.text = self.data.names[indexPath.row];
    cell.labelCount.text = self.rates[indexPath.row];
    
    return cell;
    
}

//MARK: - Delegates and datasource (pickerview)

// returns the number of 'columns' to display.
- (NSInteger)numberOfComponentsInPickerView:(UIPickerView *)pickerView {
    return 1;
}

// returns the # of rows in each component..
- (NSInteger)pickerView:(UIPickerView *)pickerView numberOfRowsInComponent:(NSInteger)component {
    return self.data.sites.count;
}

- (NSString *)pickerView:(UIPickerView *)thePickerView titleForRow:(NSInteger)row forComponent:(NSInteger)component {
    return [self.data.sites objectAtIndex:row];
}

- (void)pickerView:(UIPickerView *)pickerView didSelectRow:(NSInteger)row inComponent:(NSInteger)component __TVOS_PROHIBITED {

    NSUInteger selectedRow = [self.sitePicker selectedRowInComponent:0];
    
//    NSMutableArray *test = [[NSMutableArray alloc] initWithObjects:self.data.allRates[1], nil];
//    NSLog(@"allRates = %@",test);
    
    self.ratesPrev = [[NSMutableArray alloc] initWithObjects: self.data.allRates[selectedRow], nil];
//    NSLog(@"rates = %@",self.ratesPrev);
    NSString *ratesString = [[self.ratesPrev valueForKey:@"description"] componentsJoinedByString:@""];
//    NSLog(@"ratesString = %@", ratesString);
//    NSArray *ratesArrayPreviously = [ratesString componentsSeparatedByString:@" "];
//    NSLog(@"%@",ratesArrayPreviously);
    
//    NSArray *ratesArrayPreviously = [ratesString componentsSeparatedByCharactersInSet:
//                                    [NSCharacterSet characterSetWithCharactersInString:@"(,)\n"]];
    
    
    NSString *ratesStringPreviously = [ratesString stringByReplacingOccurrencesOfString:@"[^0-9]" withString:@" " options:NSRegularExpressionSearch range:NSMakeRange(0, [ratesString length])];
//    NSLog(@"ratesStringPreviously = %@",ratesStringPreviously);
    NSArray *arrayOfStrings = [ratesStringPreviously componentsSeparatedByString:@" "];
    //6, 12, 18
//    NSLog(@"arrayOfStrings[6] = %@", arrayOfStrings[6]);
    self.rates = [[NSMutableArray alloc] initWithObjects:[NSString stringWithFormat:@"%@",arrayOfStrings[6]],[NSString stringWithFormat:@"%@",arrayOfStrings[12]], [NSString stringWithFormat:@"%@",arrayOfStrings[18]], nil];
    
//    NSLog(@"self.rates = %@",self.rates);



    
//
//    NSArray *ratesArrayPreviously = [ratesStringPreviously componentsSeparatedByCharactersInSet:
//                                        [NSCharacterSet characterSetWithCharactersInString:@","]];
//    NSLog(@"%@",ratesArrayPreviously);


    
    
//    self.rates = [[NSMutableArray alloc] initWithObjects: @"100",@"200",@"300",nil];
//    [self.tableView reloadData];

}

@end
