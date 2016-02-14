//
//  GeneralStatisticViewController.m
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "GeneralStatisticViewController.h"
#import "GeneralStatisticTableViewCell.h"
#import "GetData.h"

@interface GeneralStatisticViewController () <UITableViewDataSource, UITableViewDelegate, UIPickerViewDataSource, UIPickerViewDelegate>

@property (strong, nonatomic) GetData *data;
@property (weak, nonatomic) IBOutlet UIPickerView *sitePicker;
@property (strong, nonatomic) NSMutableArray *rates;
@property (weak, nonatomic) IBOutlet UITableView *tableView;

@end

@implementation GeneralStatisticViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.sitePicker.delegate = self;
    self.sitePicker.dataSource = self;
    
    self.data = [[GetData alloc] init];
    [self.data getNames];
    [self.data getSites];
    [self getRates];

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
    
    self.selectedRow = [self.sitePicker selectedRowInComponent:0];
    
    [self getRates];
        
    [self.tableView reloadData];

}

//Dont know how do it in GetData?
- (void) getRates {
    
    NSString *generalRankURL = @"http://crawler.firstexperience.ru/api/v1/personrank/";
    NSString *rankWithIdURL = [NSString stringWithFormat:@"%@%@", generalRankURL, self.data.sitesID[self.selectedRow]];
    NSArray *data = [[NSArray alloc] init];
    NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:rankWithIdURL]];
    NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
    NSMutableArray *ratesJSON = [NSMutableArray array];
    data = jsonResult;
    for (id item in jsonResult)
        [ratesJSON addObject:[NSString stringWithFormat:@"%@", item[@"rank"]]];
    self.rates = ratesJSON;
}

@end