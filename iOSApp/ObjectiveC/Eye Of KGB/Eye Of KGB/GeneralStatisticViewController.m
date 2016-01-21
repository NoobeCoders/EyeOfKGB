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
    cell.labelCount.text = self.data.rates[indexPath.row];
    
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

@end
