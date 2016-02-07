//
//  SitePickerDailyStatisticViewController.m
//  Eye Of KGB
//
//  Created by bu on 05/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "SitePickerDailyStatisticViewController.h"
#import "GetData.h"

#import "DailyStatisticViewController.h"

@interface SitePickerDailyStatisticViewController () <UIPickerViewDataSource, UIPickerViewDelegate>
@property (weak, nonatomic) IBOutlet UIBarButtonItem *cancelButton;
@property (weak, nonatomic) IBOutlet UIBarButtonItem *doneButton;
@property (strong, nonatomic) GetData *data;
@property (weak, nonatomic) IBOutlet UIPickerView *sitePicker;
@end

@implementation SitePickerDailyStatisticViewController


- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.sitePicker.delegate = self;
    self.sitePicker.dataSource = self;
    
    self.data = [[GetData alloc] init];
    [self.data getSites];
    
    
    
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

- (IBAction)cancelButton:(id)sender {
    DailyStatisticViewController *dailyStatisticViewController = [self.storyboard instantiateViewControllerWithIdentifier:@"DailyStatisticViewController"];
    [self presentViewController:dailyStatisticViewController animated:YES completion:NULL];
}

- (IBAction)doneButton:(id)sender {
    DailyStatisticViewController *dailyStatisticViewController = [self.storyboard instantiateViewControllerWithIdentifier:@"DailyStatisticViewController"];
    [self presentViewController:dailyStatisticViewController animated:YES completion:NULL];
    dailyStatisticViewController.selectedRow = [self.sitePicker selectedRowInComponent:0];
    dailyStatisticViewController.isItSelected = true;
}

@end
