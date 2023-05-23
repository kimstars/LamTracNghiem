import json

question = "Câu 3: Tổ chức lực lượng hậu cần là...D\nA. việc tổ chức triển khai các lực lượng hậu cần cho phù hợp với yêu cầu, nhiệm vụ, ý định của người chỉ huy.\nB. việc thay đổi các vị trí bố trí lực lượng hậu cần cho phù hợp với yêu cầu, nhiệm vụ, ý định của người chỉ huy...\nC. việc sử dụng, bố trí, triển khai lực lượng hậu cần cho phù hợp với yêu cầu, nhiệm vụ, ý định của người chỉ huy..."


# Tách câu hỏi và đáp án
question_parts = question.split("\n")
print(question_parts)
# question_number = question_parts[0].strip()
# question_text = question_parts[1].strip()

# answer_options = question_parts[2:]
# answers = []
# correct_answer = ""
# for option in answer_options:
#     if option.endswith("D"):
#         correct_answer = option.strip("D").strip()
#     answers.append(option.strip())

# # Tạo đối tượng JSON
# question_obj = {
#     "question_number": question_number,
#     "question_text": question_text,
#     "answers": answers,
#     "correct_answer": correct_answer
# }

# # Chuyển đối tượng thành chuỗi JSON
# json_data = json.dumps(question_obj, ensure_ascii=False)

# # In ra chuỗi JSON
# print(json_data)