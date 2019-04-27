using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Core.Model;
using Core.Base;
using System.Collections.Generic;

namespace Core.Business
{
    public class TestGroupBiz : _Service
    {
        public async Task<Json> Insert(TestGroup model)
        {
            try
            {
                var multiResult = await base.Data.QueryMultipleAsync
                        (
                           "[Test].[SP_TestGroup_Insert]",

                           new
                           {
                               model.UserId,
                               model.Code,
                               model.Title,
                               model.ParentId,
                               model.Description
                           },

                           commandType: CommandType.StoredProcedure
                       );
                var message = multiResult.Read<Message>().FirstOrDefault();
                return new Json(null, message.Success, null, null, new List<string>() { $" {message.Text} , {message.Title} " });
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }
        public async Task<Json> Update(TestGroup model)
        {
            try
            {
                var multiResult = await base.Data.QueryMultipleAsync
                        (
                           "[Test].[SP_TestGroup_Update]",

                           new
                           {
                               model.Id,
                               model.UserId,
                               model.Title,
                               model.ParentId,
                               model.Description
                           },

                           commandType: CommandType.StoredProcedure
                       );
                var message = multiResult.Read<Message>().FirstOrDefault();
                return new Json(null, message.Success, null, null, new List<string>() { $" {message.Text} , {message.Title} " });
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }
        public async Task<Json> Delete(long Id)
        {
            try
            {
                var multiResult = await base.Data.QueryMultipleAsync
                       (
                           "[Test].[SP_TestGroup_Delete]",

                           new { Id = Id },

                           commandType: CommandType.StoredProcedure
                       );

                var message = multiResult.Read<Message>().FirstOrDefault();
                return new Json(null, message.Success, null, null, new List<string>() { $" {message.Text} , {message.Title} " });
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در ذخیره اطلاعات");
            }
        }


        public async Task<Json> GetAllView()
        {
            try
            {

                IEnumerable<TestGroupView> TestGroupViewList = await base.Data.QueryAsync<TestGroupView>
                    (
                        "[Test].[SP_TestGroup_GetAllView]",

                        new { },

                        commandType: CommandType.StoredProcedure
                    );


                return new Json(TestGroupViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }

        public async Task<Json> LastLevelGetAllView()
        {
            try
            {

                IEnumerable<TestGroupView> TestGroupViewList = await base.Data.QueryAsync<TestGroupView>
                    (
                        "[Test].[SP_TestGroup_LastLevelGetAllView]",

                        new { },

                        commandType: CommandType.StoredProcedure
                    );


                return new Json(TestGroupViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }
        public async Task<Json> TestGroupWithSubSetAndItems(int branchId)
        {
            try
            {

                IEnumerable<TestGroupWithItemsView> TestGroupViewList = await base.Data.QueryAsync<TestGroupWithItemsView>
                    (
                        "[Test].[SP_TestGroup_WithSubSetAndItemsByBranchId]",

                        new { BranchId = branchId },

                        commandType: CommandType.StoredProcedure
                    );

                var testList = CreateMenu(TestGroupViewList.ToList());

                return new Json(testList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }
        public async Task<Json> GetAll()
        {
            try
            {

                IEnumerable<TestGroup> TestGroupViewList = await base.Data.QueryAsync<TestGroup>
                    (
                        "[Test].[SP_TestGroup_GetAll]",

                        new { },

                        commandType: CommandType.StoredProcedure
                    );


                return new Json(TestGroupViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }

        public async Task<Json> GetTestGroupWithoutSubset() // تمامی گروه آزمون هایی که در گروه آزمون زیر مجموعه نداردند
        {
            try
            {

                IEnumerable<TestGroupView> TestGroupViewList = await base.Data.QueryAsync<TestGroupView>
                    (
                        "[Test].[SP_TestGroup_GetWithoutSubset]",

                        new { },

                        commandType: CommandType.StoredProcedure
                    );


                return new Json(TestGroupViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }
        public async Task<Json> GetTestGroupRoot() // تمامی گروه آزمون هایی که در گروه آزمون زیر مجموعه نداردند
        {
            try
            {

                IEnumerable<TestGroupView> TestGroupViewList = await base.Data.QueryAsync<TestGroupView>
                    (
                        "[Test].[SP_TestGroup_GetRoot]",

                        new { },

                        commandType: CommandType.StoredProcedure
                    );
                TestGroupViewList.ToList().ForEach(f =>
                {
                    if (f.Title.Trim().Contains(" "))
                    {
                        var splitTitle = f.Title.Split(' ');
                        f.TitleAbbreviation = string.Empty;
                        splitTitle.ToList().ForEach(s =>
                        {
                            if (s == "&")
                            {
                                f.TitleAbbreviation += " ";
                            }
                            f.TitleAbbreviation += s[0].ToString().ToUpper();
                            if (s == "&")
                            {
                                f.TitleAbbreviation += " ";
                            }
                        });

                    }
                    else
                    {
                        f.TitleAbbreviation = f.Title;
                    }
                });

                return new Json(TestGroupViewList);
            }
            catch (Exception e)
            {
                return new Json(false, "خطا در واکشی اطلاعات");
            }
        }
        private List<TestGroupWithSubSetAndItems> CreateMenu(List<TestGroupWithItemsView> testList)
        {
            List<TestGroupWithSubSetAndItems> testView = new List<TestGroupWithSubSetAndItems>();

            testList.Where(w => w.MasterTestGroupId == null)
                .Select(s => new { s.TestGroupId, s.TestGroupTitle })
                .Distinct()
                .ToList().ForEach(g =>   // null => نود سر گروه هست
            {
                //create root node to new object
                var testGroupItems = new Model.TestGroupWithSubSetAndItems()
                {
                    Id = g.TestGroupId,
                    Title = g.TestGroupTitle,
                    subTestItemList = new List<TestItemView>()
                };

                var testItems = testList.Where(w => w.TestGroupId == g.TestGroupId);

                if (testItems.Count() > 0)
                {
                    //add to list
                    testItems.ToList().ForEach(itm =>
                    {

                        testGroupItems.subTestItemList.Add(new Model.TestItemView
                        {
                            Id = itm.Id,
                            HoldingDate = itm.HoldingDate,
                            PersianHoldingDate = itm.HoldingDate.ToPersianShortDate(),
                            HoldingTime = itm.HoldingTime,
                            BranchName = itm.BranchName,
                            TestGroupId = itm.TestGroupId,
                            TestGroupTitle = itm.TestGroupTitle,
                            PercentEmployer = itm.PercentEmployer,
                            RegistrationDeadline = itm.RegistrationDeadline,
                            Title = itm.Title,
                            FullTitle = $"{testGroupItems.Title} - {itm.Title}",
                            Cost = itm.Cost,
                            DeadlineCancelling = itm.DeadlineCancelling,
                            PersianDeadlineCancelling = itm.DeadlineCancelling.ToPersianShortDate(),
                            PersianRegistrationDeadline = itm.RegistrationDeadline.ToPersianShortDate(),
                            TestItemStatusTitle = itm.TestItemStatusTitle,
                            Capacity = itm.Capacity
                        });

                    });

                    testView.Add(testGroupItems);
                }

            });



            testList.Where(w => w.MasterTestGroupId != null)
                .Select(s => new { s.MasterTestGroupId, s.MasterTestGroupTitle })
                .Distinct().ToList().ForEach(g =>
                {
                    //create root node to new object
                    var testGroupItems = new Model.TestGroupWithSubSetAndItems()
                    {
                        Id = g.MasterTestGroupId.Value,
                        Title = g.MasterTestGroupTitle,
                        subTestGroupList = new List<Model.TestGroupWithSubSetAndItems>()
                    };

                    var testGroup = testList
                                            .Where(w => w.MasterTestGroupId == g.MasterTestGroupId)
                                            .Select(s => new { s.TestGroupId, s.TestGroupTitle });
                    if (testGroup.Count() > 0)
                    {

                        testGroup.Distinct().ToList().ForEach(sg =>
                                        {
                                            testGroupItems.subTestGroupList.Add(new Model.TestGroupWithSubSetAndItems
                                            {
                                                Id = sg.TestGroupId,
                                                Title = sg.TestGroupTitle,
                                                subTestItemList = new List<TestItemView>()
                                            });

                                            var testItems = testList.Where(w => w.TestGroupId == sg.TestGroupId);
                                            if (testItems.Count() > 0)
                                            {
                                                testItems.ToList().ForEach(itm =>
                                                {
                                                    testGroupItems.subTestGroupList.FirstOrDefault(w => w.Id == sg.TestGroupId).subTestItemList.Add(new Model.TestItemView
                                                    {
                                                        Id = itm.Id,
                                                        HoldingDate = itm.HoldingDate,
                                                        PersianHoldingDate = itm.HoldingDate.ToPersianShortDate(),
                                                        HoldingTime = itm.HoldingTime,
                                                        BranchName = itm.BranchName,
                                                        TestGroupId = itm.TestGroupId,
                                                        TestGroupTitle = itm.TestGroupTitle,
                                                        PercentEmployer = itm.PercentEmployer,
                                                        RegistrationDeadline = itm.RegistrationDeadline,
                                                        Title = itm.Title,
                                                        FullTitle = $"{testGroupItems.Title} - {testGroupItems.subTestGroupList.FirstOrDefault().Title} - {itm.Title}",
                                                        Cost = itm.Cost,
                                                        DeadlineCancelling = itm.DeadlineCancelling,
                                                        PersianDeadlineCancelling = itm.DeadlineCancelling.ToPersianShortDate(),
                                                        PersianRegistrationDeadline = itm.RegistrationDeadline.ToPersianShortDate(),
                                                        TestItemStatusTitle = itm.TestItemStatusTitle,
                                                        Capacity = itm.Capacity
                                                    });
                                                });

                                            }
                                        });
                    }
                    testView.Add(testGroupItems);

                });


            return testView;
        }



    }
}